using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BaseContext context;

        public Repository(BaseContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = context.Set<T>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }
            return await query.CountAsync();
        }

        public async Task<List<T>> GetAsync(int page = 1, int count = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = context.Set<T>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            return await query.Skip((page - 1) * count).Take(count).ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = context.Set<T>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
