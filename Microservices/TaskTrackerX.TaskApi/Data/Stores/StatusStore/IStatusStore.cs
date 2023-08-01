using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data.Stores.StatusStore
{
    public interface IStatusStore : IStoreBase<Status>
    {
        IQueryable<Status> Status { get; }

        Task<Status?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<Status?> FindByNameAsync(string name,
            CancellationToken cancellationToken = default);
    }
}
