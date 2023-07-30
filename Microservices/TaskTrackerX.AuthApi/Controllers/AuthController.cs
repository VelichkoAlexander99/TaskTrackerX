using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Services;
using TaskTrackerX.AuthApi.Services.AuthService;
using TaskTrackerX.AuthApi.Services.UserService;

namespace TaskTrackerX.AuthApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(
            IAuthService authService, 
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.AuthenticateAsync(loginDto);
            
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userService.GetUserByIdAsync(userId);

            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);

            return new ObjectResult(result)
            { 
                StatusCode = result.StatusCode
            };
        }

        [HttpPost("change-name")]
        [Authorize]
        public async Task<IActionResult> ChangeName(ChangeNameDto changeNameDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.ChangeNameAsync(userId, changeNameDto);
            
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}
