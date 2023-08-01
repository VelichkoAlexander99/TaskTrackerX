using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Managers.StatusManager
{
    public interface IStatusManager
    {
        Task<IEnumerable<Status>> GetListAsync();
        Task<Status?> FindByIdAsync(Guid id);
        Task<Status?> FindByNameAsync(string statusName);
        Task<Result> CreateAsync(Status status);
        Task<Result> UpdateAsync(Status status);
        Task<Result> DeleteAsync(Status status);
    }
}