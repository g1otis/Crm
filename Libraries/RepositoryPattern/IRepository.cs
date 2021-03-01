using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Libraries.RepositoryPattern
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(TId id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        void Delete(TEntity entityToDelete);
        void Delete(TId id);

        Task<int> SaveChangesAsync();
    }
}
