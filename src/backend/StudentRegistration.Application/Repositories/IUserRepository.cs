using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        void DeleteTokenByName(string refreshToken);
    }
}
