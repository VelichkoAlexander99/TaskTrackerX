using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<UserDto>> FindByIdAsync(Guid userId);
    }
}
