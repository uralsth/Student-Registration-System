using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Helpers.Interfaces
{
	public interface IAuthHelper
	{
		void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
		bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
		string CreateToken(User user);
		RefreshToken GenerateRefreshToken();
		void SetRefreshToken(RefreshToken newRefreshToken, User user);
		string GetIPAddress();
		string GetMacAddress();
	}
}
