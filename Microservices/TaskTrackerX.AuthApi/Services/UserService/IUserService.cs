using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.UserService
{
    public interface IUserService
    {
        // Получить список всех пользователей
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync();

        // Получить пользователя по его идентификатору
        Task<ApiResponse<UserWithRolesDto>> GetUserByIdAsync(string userId);

        // Создать нового пользователя
        Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        // Обновить существующего пользователя
        Task<ApiResponse<UserDto>> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);

        // Удалить пользователя по его идентификатору
        Task<ApiResponse<bool>> DeleteUserAsync(string userId);

        // Назначить роли пользователю
        Task<ApiResponse<bool>> AssignRolesToUserAsync(string userId, AssignRolesDto assignRolesDto);
    }
}
