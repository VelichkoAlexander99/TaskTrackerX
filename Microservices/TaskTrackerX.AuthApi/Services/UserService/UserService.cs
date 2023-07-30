using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<User> userManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return ApiResponse<IEnumerable<UserDto>>.Success(userDtos);
        }

        public async Task<ApiResponse<UserWithRolesDto>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<UserWithRolesDto>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            var rolesUser = await _userManager.GetRolesAsync(user);

            return ApiResponse<UserWithRolesDto>.Success(_mapper.Map<UserWithRolesDto>(user, opts =>
            {
                opts.AfterMap((o, d) =>
                {
                    d.Roles = rolesUser;
                });
            }));
        }

        public async Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
                return ApiResponse<UserDto>.Fail(errors: result.Errors.Select(t => t.Description).ToArray());
            
            return ApiResponse<UserDto>.Success(_mapper.Map<UserDto>(user), HttpStatusCode.Created);
        }

        public async Task<ApiResponse<UserDto>> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<UserDto>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            user.Name = updateUserDto.Name;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ApiResponse<UserDto>.Fail(errors: result.Errors.Select(t => t.Description).ToArray());

            return ApiResponse<UserDto>.Success(_mapper.Map<UserDto>(user));
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<bool>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return ApiResponse<bool>.Fail(errors: result.Errors.Select(t => t.Description).ToArray());

            return ApiResponse<bool>.Success(result.Succeeded);
        }

        public async Task<ApiResponse<bool>> AssignRolesToUserAsync(string userId, AssignRolesDto assignRolesDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<bool>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = assignRolesDto.Roles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(assignRolesDto.Roles);

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return ApiResponse<bool>.Success(true);
        }
    }
}
