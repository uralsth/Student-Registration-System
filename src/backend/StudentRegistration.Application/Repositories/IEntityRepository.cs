using Microsoft.EntityFrameworkCore;

namespace StudentRegistration.Application.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        Task<T> Add(T Entity);
        Task<T> Delete(int id);
        Task<T> Update(T updatedEntity);
        IQueryable<T> Table { get; }
    }
}
