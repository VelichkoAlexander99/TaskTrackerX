using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<AccessTokenDto>> AuthenticateAsync(LoginDto loginDto);

        Task<ApiResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);

        Task<ApiResponse<bool>> ChangeNameAsync(string userId, ChangeNameDto changeNameDto);
    }
}
