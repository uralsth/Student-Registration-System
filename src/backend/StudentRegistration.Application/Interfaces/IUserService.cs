using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Interfaces
{
	public interface IUserService
	{
		User AddUser(UserDto user);
		string CreateAndGetToken(User user);
		User GetByUserName(string userName);
		bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
		Task<User> Update(User user);
		void DeleteByToken(string refreshToken);
		Task Logout(User user);
	}
}
