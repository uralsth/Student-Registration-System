using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Repositories
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }


        IEntityRepository<Project> Projects { get; }
        IUserRepository Users { get; }

        IEntityRepository<RefreshToken> RefreshTokens { get; }

        void Save();
    }
}
