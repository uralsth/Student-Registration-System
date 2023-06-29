using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Infrastructure.Context;

namespace StudentRegistration.Infrastructure.Repositories
{
	public class StudentRepository : EntityRepository<Student>, IStudentRepository
	{
		public StudentRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<IList<Student>> GetAllStudentWithProject()
		{
			return await _context.Set<Student>().Include(u => u.Projects).ToListAsync();
		}

		public async Task<Student> GetStudentWithProject(int id)
		{
			return await _context.Set<Student>().Include(u => u.Projects).FirstOrDefaultAsync(a => a.Id == id);
		}

	}
}
