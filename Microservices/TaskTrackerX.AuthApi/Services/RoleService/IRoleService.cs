using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.RoleService
{
    public interface IRoleService
    {
        Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRolesAsync();
    }
}
