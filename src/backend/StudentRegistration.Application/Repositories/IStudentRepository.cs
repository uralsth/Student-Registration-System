using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Repositories
{
    public interface IStudentRepository : IEntityRepository<Student>
    {
        Task<Student> GetStudentWithProject(int id);
        Task<IList<Student>> GetAllStudentWithProject();


    }
}
