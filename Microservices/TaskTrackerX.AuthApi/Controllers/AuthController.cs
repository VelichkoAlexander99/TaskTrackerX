using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Security.Claims;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Extensions;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Services;
using TaskTrackerX.AuthApi.Services.JwtTokenGenerator;

namespace TaskTrackerX.AuthApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByNameAsync(loginDto.Login);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return this.ToApiResponseError(401, errors: ErrorDescriber.InvalidUserLoginOrPassword());

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenGenerator.GenerateToken(user, roles);

            return this.ToApiResponse(new AccessTokenDto
            {
                Token = accessToken
            });
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var convertUserId = Guid.Parse(userId);

            var userFind = await _userManager.Users
                .Include(t => t.Roles)
                .FirstOrDefaultAsync(t => t.Id.Equals(convertUserId));
            if (userFind == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            return this.ToApiResponse(_mapper.Map<UserDto>(userFind));
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
                throw new ArgumentNullException(nameof(changePasswordDto));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            return result.Succeeded ? 
                this.ToApiResponse(true) :
                this.ToApiResponseError(errors: result.Errors.ConvertToErrorInfo());
        }

        [HttpPost("change-name")]
        [Authorize]
        public async Task<IActionResult> ChangeName(ChangeNameDto changeNameDto)
        {
            if (changeNameDto == null)
                throw new ArgumentNullException(nameof(changeNameDto));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidUser());

            user.Name = changeNameDto.NewName;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ?
                this.ToApiResponse(true) :
                this.ToApiResponseError(errors: result.Errors.ConvertToErrorInfo());
        }
    }
}
