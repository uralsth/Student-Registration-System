using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Infrastructure.Context;

namespace StudentRegistration.Infrastructure.Repositories
{
	public class EntityRepository<T> : IEntityRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;


		public EntityRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();

		}

		public async Task<T> Add(T Entity)
		{
			await _dbSet.AddAsync(Entity);
			return Entity;
		}


		public async Task<T> Delete(int id)
		{
			T entity = await _dbSet.FindAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
			}
			return entity;
		}

		public async Task<IList<T>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T> Update(T updatedEntity)
		{
			var entity = _dbSet.Attach(updatedEntity);
			entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return updatedEntity;
		}

		#region properties
		public IQueryable<T> Table => _dbSet;

		#endregion

	}
}
