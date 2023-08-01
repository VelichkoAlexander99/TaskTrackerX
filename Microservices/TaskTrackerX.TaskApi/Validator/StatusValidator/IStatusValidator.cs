using Microsoft.AspNetCore.Identity;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Validator.StatusValidator
{
    public interface IStatusValidator
    {
        Task<Result> ValidateAsync(IStatusManager manager, Status role);
    }
}
