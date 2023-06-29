using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
//using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Helpers.Interfaces;

namespace StudentRegistration.API.Helpers
{
	public class AuthHelper : IAuthHelper
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;

		}
		public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, "Admin")
			};

			// same key is used to sign and verify the integrity of the token
			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			// encapsulate key and signing alorithim
			// key and cred passed to JwtSecurityToken constructor
			// then token is configured to be signed using specified key and algorit
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			// contains various properties such as claims, issuer, audience,
			// expiration time, and signing credentials.
			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			//methods to generate JWTs from JwtSecurityToken instances,
			//validate and verify JWTs, and read the information contained within a JWT.
			//The WriteToken used to generate the actual string
			//representation of a JWT from a JwtSecurityToken instance.
			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}

		public RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Expires = DateTime.Now.AddDays(7),
				Created = DateTime.Now,
				IpAddress = GetIPAddress(),
				MacAddress = GetMacAddress(),
				//DeviceId = GetDeviceId(),
				IsActive = true
			};
			return refreshToken;
		}

		//private int? GetDeviceId()
		//{
		//	return _httpContextAccessor.HttpContext.Connection.Id();
		//}

		public void SetRefreshToken(RefreshToken newRefreshToken, User user)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = newRefreshToken.Expires
			};
			var httpContext = _httpContextAccessor.HttpContext;
			httpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
			user.TokenExpires = newRefreshToken.Expires;
			user.TokenCreated = newRefreshToken.Created;
			user.RefreshToken = newRefreshToken.Token;
		}

		public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}

		public string GetIPAddress()
		{
			string ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
			return ipAddress;
		}
		public string GetMacAddress()
		{
			var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
				.Where(nic => nic.OperationalStatus == OperationalStatus.Up && !nic.GetPhysicalAddress().Equals(PhysicalAddress.None));

			var firstInterface = networkInterfaces.FirstOrDefault();
			if (firstInterface != null)
			{
				return firstInterface.GetPhysicalAddress().ToString();
			}

			return string.Empty;
		}


	}
}
