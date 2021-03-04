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
        Task<IQueryable<TEntity>> GetAsync();

        void Delete(TEntity entityToDelete);
        void Delete(TId id);

        Task<int> SaveChangesAsync();
    }
}
