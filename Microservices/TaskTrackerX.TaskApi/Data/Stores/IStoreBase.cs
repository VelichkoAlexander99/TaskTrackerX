using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores
{
    public interface IStoreBase<TEntity> 
        where TEntity : class
    {
        Task<Result> CreateAsync(TEntity entity, 
            CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(TEntity entity, 
            CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(TEntity entity, 
            CancellationToken cancellationToken = default);
    }
}