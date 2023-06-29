using System.Security.Claims;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Helpers.Interfaces;

namespace StudentRegistration.Application.Services
{

    public class UserService : IUserService
	{
		private readonly IAuthHelper _authHelper;
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEntityRepository<RefreshToken> _refreshTokenRepository;

		public UserService(IAuthHelper authHelper, IUserRepository userRepository, IUnitOfWork unitOfWork, IEntityRepository<RefreshToken> refreshTokenRepository)
		{
			_authHelper = authHelper;
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
			_refreshTokenRepository = refreshTokenRepository;
		}

		public User AddUser(UserDto user)
		{
			_authHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
			User newUser = new User
			{
				UserName = user.Username,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};
			_userRepository.Add(newUser);
			_unitOfWork.Save();
			return newUser;
		}

		public async Task<User> Update(User user)
		{
			User updatedUser = await _userRepository.Update(user);
			_unitOfWork.Save();
			return updatedUser;
		}

		public string CreateAndGetToken(User user)
		{
			string token = _authHelper.CreateToken(user);

			var refreshToken = _authHelper.GenerateRefreshToken();
			_authHelper.SetRefreshToken(refreshToken, user);
			_userRepository.Update(user);
			_refreshTokenRepository.Add(refreshToken);
			_unitOfWork.Save();
			return token;
		}

		public User GetByUserName(string userName)
		{
			User user = _unitOfWork.Users.Table.First(x => x.UserName == userName);
			return user;
		}

		public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			return _authHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
		}

		public void DeleteByToken(string refreshToken)
		{
			_userRepository.DeleteTokenByName(refreshToken);
		}
		public async Task Logout(User user)
		{
			user.RefreshToken = null;
			await _userRepository.Update(user);
			_unitOfWork.Save();
		}
		//public string GetMyName()
		//{
		//	var result = string.Empty;
		//	if (_httpContextAccessor.HttpContext != null)
		//	{
		//		result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
		//	}
		//	return result;
		//}

	}
}
