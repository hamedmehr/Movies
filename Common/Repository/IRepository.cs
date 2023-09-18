using Data;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> GetByIdAsync(long id);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<List<T>> GetAsync(int page = 1, int count = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<bool> RemoveAsync(T entity);
        Task<int> SaveChangeAsync();
    }
}