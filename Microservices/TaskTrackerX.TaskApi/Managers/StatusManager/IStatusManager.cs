using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Query;

namespace TaskTrackerX.TaskApi.Managers.StatusManager
{
    public interface IStatusManager
    {
        Task<PagedResult<Status>> GetListAsync(FilterOptions<Status> filterOptions);
        Task<Status?> FindByIdAsync(Guid id);
        Task<Status?> FindByNameAsync(string statusName);
        Task<Result> CreateAsync(Status status);
        Task<Result> UpdateAsync(Status status);
        Task<Result> DeleteAsync(Status status);
    }
}