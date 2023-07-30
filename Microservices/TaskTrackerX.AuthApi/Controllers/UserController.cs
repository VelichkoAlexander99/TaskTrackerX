using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.Services.RoleService;
using TaskTrackerX.AuthApi.Services.UserService;

namespace TaskTrackerX.AuthApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(
            IUserService userService, 
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAsync(createUserDto);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(id, updateUserDto);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpPost("{id}/roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRolesToUser(string id, AssignRolesDto assignRolesDto)
        {
            var result = await _userService.AssignRolesToUserAsync(id, assignRolesDto);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}
