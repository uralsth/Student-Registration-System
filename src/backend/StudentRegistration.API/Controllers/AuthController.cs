using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentRegistration.API.Middlewares;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Helpers.Interfaces;

namespace StudentRegistration.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public static User user = new User();
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;

		private readonly List<string> _blackListedToken;
		private string token;

		public AuthController(IConfiguration configuration, IUserService userService)
		{
			_configuration = configuration;
			_userService = userService;
			_blackListedToken = new List<string>();
		}

		[HttpPost("Login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			User user = _userService.GetByUserName(request.Username);
			if (user.UserName == null)
			{
				return BadRequest("user not found");
			}

			if (!_userService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				return BadRequest("wrong password.");
			};

			string token = _userService.CreateAndGetToken(user);
			return Ok(token);
		}

		[HttpPost("refresh-token")]
		public async Task<ActionResult<string>> RefreshToken(string userName)
		{
			User user = _userService.GetByUserName(userName);
			if (user.UserName == null)
			{
				return BadRequest("User doesn't exist");
			}

			var refreshToken = Request.Cookies["refreshToken"];
			if (!user.RefreshToken.Equals(refreshToken))
			{
				return Unauthorized("Invalid Refresh Token.");
			}
			else if (user.TokenExpires < DateTime.Now)
			{
				return Unauthorized("Token expired");
			}
			token = _userService.CreateAndGetToken(user);
			return Ok(token);
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDto request)
		{
			User user = _userService.AddUser(request);
			return Ok(user);
		}

		[HttpPost("Logout")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Logout()
		{
			// Retrieve the current user's access token
			string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];


			BlackListToken(accessToken);
			// Perform other logout-related action

			var user = User.Identity.Name;
			User user1 = _userService.GetByUserName(user);
			user1.RefreshToken = null;
			user1.TokenCreated = null;
			user1.TokenExpires = null;

			await _userService.Update(user1); // Save the changes to the user

			var refreshToken = Request.Cookies["refreshToken"];
			if (!string.IsNullOrEmpty(refreshToken))
			{

				// Delete the refresh token cookie
				Response.Cookies.Delete("refreshToken");

				return Ok("Logged out successfully.");
			}

			return BadRequest("Invalid or missing refresh token.");
		}

		private void BlackListToken(string? accessToken)
		{
			_blackListedToken.Add(accessToken);
		}
	}


	#region Logout_Parashar
	//[Authorize(Roles = "Admin")]
	//[HttpPost]
	//public async Task<IActionResult> Logout()
	//{
	//	var user = User.Identity.Name;
	//	User user1 = _userService.GetByUserName(user);
	//	user1.RefreshToken = null;
	//	user1.TokenCreated = null;
	//	user1.TokenExpires = null;

	//	await _userService.Update(user1); // Save the changes to the user

	//	var refreshToken = Request.Cookies["refreshToken"];
	//	if (!string.IsNullOrEmpty(refreshToken))
	//	{
	//		_userService.DeleteByToken(refreshToken);

	//		await _userService.Logout(user1);

	//		// Set the access token expiration to a past date
	//		var tokenValidationParameters = new TokenValidationParameters
	//		{
	//			ValidateIssuerSigningKey = true,
	//			IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
	//			ValidateIssuer = false,
	//			ValidateAudience = false
	//		};
	//		var tokenHandler = new JwtSecurityTokenHandler();
	//		if (tokenHandler.CanReadToken(token))
	//		{
	//			var jwtToken = tokenHandler.ReadJwtToken(token);
	//			var claims = jwtToken.Claims.ToList();
	//			claims.Add(new Claim("exp", new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));

	//			var newToken = new JwtSecurityToken(
	//				claims: claims,
	//				signingCredentials: jwtToken.SigningCredentials
	//			);

	//			var encodedToken = tokenHandler.WriteToken(newToken);
	//			Response.Cookies.Delete("jwtToken");
	//			Response.Cookies.Append("jwtToken", encodedToken, new CookieOptions
	//			{
	//				HttpOnly = true,
	//				Expires = DateTime.Now.AddMinutes(12)
	//			});
	//		}

	//		// Delete the refresh token cookie
	//		Response.Cookies.Delete("refreshToken");

	//		return Ok("Logged out successfully.");
	//	}

	//	return BadRequest("Invalid or missing refresh token.");
	//}
	//[HttpGet]
	//[Authorize]
	//public ActionResult<string> GetMe()
	//{
	//	//var userName = _userService.GetMyName();
	//	//return Ok(userName);

	//	var username = User.Identity?.Name;
	//	var userName2 = User.FindFirstValue(ClaimTypes.Name);
	//	var role = User.FindFirstValue(ClaimTypes.Role);
	//	return Ok(new { username, userName2, role });
	//}
	#endregion
}


