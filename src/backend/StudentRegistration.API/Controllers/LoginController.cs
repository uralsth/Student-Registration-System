//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using StudentRegistration.Models;

//namespace StudentRegistration.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class LoginController : ControllerBase
//	{
//		private readonly IConfiguration _config;

//		public LoginController(IConfiguration configuration)
//        {
//			_config = configuration;
//		}

//		private  User AuthenticateUser(User user)
//		{
//			User _user = null;
//			if(user.UserName == "admin" && user.Password == "12345")
//			{
//				_user = new User { UserName = "Ural Shresthja" };
//			}
//			return user;
//		}


//		private string GenerateToken(User user)
//		{
//			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//			var token = new JwtSecurityToken(_config["Jwt:Issuers"],_config["Jwt:Audience"],null,
//				expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
//			return new JwtSecurityTokenHandler().WriteToken(token);	
//		}

//		[AllowAnonymous]
//		[HttpPost]
//		public IActionResult Login(User user)
//		{
//			IActionResult response = Unauthorized();
//			var user_ = AuthenticateUser(user);
//			if (user_ != null)
//			{
//				var token = GenerateToken(user_);
//				response = Ok(new {token = token});
//			}
//			return response;
//		}
//    }
//}
