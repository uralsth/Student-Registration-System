using StudentRegistration.Domain.Entities;
using StudentRegistration.Infrastructure.Context;
using StudentRegistration.Infrastructure.Repositories;
using StudentRegistration.Application.Repositories;

namespace StudentRegistration.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		private IStudentRepository _students;
		private IEntityRepository<Project> _projects;
		private IUserRepository _users;
		private IEntityRepository<RefreshToken> _refreshTokens;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			//Student = new StudentRepository(_context);
		}

		//public IStudentRepository Student { get; private set; }

		public IStudentRepository Students => _students ??= new StudentRepository(_context);
		public IEntityRepository<Project> Projects => _projects ??= new EntityRepository<Project>(_context);

		public IUserRepository Users => _users ??= new UserRepository(_context);

		public IEntityRepository<RefreshToken> RefreshTokens => _refreshTokens ??= new EntityRepository<RefreshToken>(_context);


		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
