using System;
using System.Threading.Tasks;

namespace Domain
{
    public interface IAsyncRepository<TEntity>
    {
        Task<TEntity> FindByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);

    }
}