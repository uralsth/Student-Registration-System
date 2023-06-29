using StudentRegistration.Application.Repositories;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Infrastructure.Context;

namespace StudentRegistration.Infrastructure.Repositories
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context)
		{
		}

		public void DeleteTokenByName(string refreshToken)
		{
			var refreshTokenEntity = _context.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);
			if (refreshTokenEntity != null)
			{
				_context.RefreshTokens.Remove(refreshTokenEntity);
				_context.SaveChanges();
			}
		}
	}
}
