using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.SeedWork
{
    public interface IRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> AddAsync(TEntity entity);
        void Delete(TEntity entityToDelete);
        Task DeleteAsync(Guid id);
        Task<TEntity> GetAsync(Guid id);
        Task<IQueryable<TEntity>> GetAsync();
        Task UpdateAsync(TEntity entity);
    }
}
