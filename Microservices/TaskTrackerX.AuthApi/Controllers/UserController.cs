using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Linq;
using System.Net;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Extensions;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Models.Options;
using TaskTrackerX.AuthApi.Models.Query;
using TaskTrackerX.AuthApi.Services;

namespace TaskTrackerX.AuthApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserWithRoleService _customService;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<SettingOptions> _options;
        private readonly IMapper _mapper;

        public UserController(
            UserWithRoleService customService,
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<User> userManager,
            IOptions<SettingOptions> options,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _customService = customService;
            _userManager = userManager;
            _options = options;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParametersDto filterOptions)
        {
            var listResult = await _customService
                .GetQueryUsersWithRoles()
                .GetPagedAsync(filterOptions.ConvertToFilterOptions(_options));

            return this.ToApiResponse(_mapper.Map<PagedResult<UserDto>>(listResult));
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindUserById(Guid userId)
        {
            var userFind = await _customService
                .GetQueryUsersWithRoles()
                .FirstOrDefaultAsync(t => t.Id.Equals(userId));
            if (userFind == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            return this.ToApiResponse(_mapper.Map<UserDto>(userFind));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                throw new ArgumentNullException(nameof(createUserDto));

            var userCreated = _mapper.Map<User>(createUserDto);

            var createdResult = await _userManager.CreateAsync(userCreated, createUserDto.Password);
            if (!createdResult.Succeeded)
                return this.ToApiResponseError(errors: createdResult.Errors.ConvertToErrorInfo());

            var roleUser = await _userManager.AddToRoleAsync(userCreated, "User");
            if (!roleUser.Succeeded)
                return this.ToApiResponseError(errors: roleUser.Errors.ConvertToErrorInfo());

            return this.ToApiResponse(_mapper.Map<UserDto>(userCreated), 201);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
                throw new ArgumentNullException(nameof(updateUserDto));

            var userUpdate = await _userManager.FindByIdAsync(userId);
            if (userUpdate == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            if (!string.IsNullOrEmpty(updateUserDto.Name))
                userUpdate.Name = updateUserDto.Name;

            if (!string.IsNullOrEmpty(updateUserDto.Login))
                userUpdate.UserName = updateUserDto.Login;

            var updateResult = await _userManager.UpdateAsync(userUpdate);

            return updateResult.Succeeded ?
                await FindUserById(Guid.Parse(userId)) :
                this.ToApiResponseError(errors: updateResult.Errors.ConvertToErrorInfo());
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userDelete = await _userManager.FindByIdAsync(userId);
            if (userDelete == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            var deleteResult = await _userManager.DeleteAsync(userDelete);

            return deleteResult.Succeeded ?
                this.ToApiResponse(true) :
                this.ToApiResponseError(errors: deleteResult.Errors.ConvertToErrorInfo());
        }

        [HttpPost("{userId}/set-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRolesToUser(string userId, [FromBody] AssignRolesDto assignRolesDto)
        {
            if (assignRolesDto == null)
                throw new ArgumentNullException(nameof(assignRolesDto));

            var userFind = await _userManager.FindByIdAsync(userId);
            if (userFind == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            var roleFind = await _roleManager.FindByIdAsync(assignRolesDto.RoleId);
            if (roleFind == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidRole());

            var currentRoles = await _userManager.GetRolesAsync(userFind);
            await _userManager.RemoveFromRolesAsync(userFind, currentRoles);
            
            var roleResult = await _userManager.AddToRoleAsync(userFind, roleFind.Name);

            return roleResult.Succeeded ?
                 this.ToApiResponse(true) :
                 this.ToApiResponseError(errors: roleResult.Errors.ConvertToErrorInfo());
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles([FromQuery] QueryParametersDto filterOptions)
        {
            var listResult = await _roleManager.Roles
                .GetPagedAsync(filterOptions.ConvertToFilterOptions(_options));

            return this.ToApiResponse(_mapper.Map<PagedResult<RoleDto>>(listResult));
        }
    }
}
