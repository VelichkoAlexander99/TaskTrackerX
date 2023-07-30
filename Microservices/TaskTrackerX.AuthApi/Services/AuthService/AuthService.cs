using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TaskTrackerX.AuthApi.DTOs.Incoming;
using TaskTrackerX.AuthApi.DTOs.Outgoing;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Services.JwtTokenGenerator;

namespace TaskTrackerX.AuthApi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ApiResponse<AccessTokenDto>> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return ApiResponse<AccessTokenDto>.Fail(HttpStatusCode.Unauthorized, "Wrong login or password");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenGenerator.GenerateToken(user, roles);

            return ApiResponse<AccessTokenDto>.Success(new AccessTokenDto
            {
                Token = accessToken
            });
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<bool>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
                return ApiResponse<bool>.Fail(errors: result.Errors.Select(t => t.Description).ToArray());

            return ApiResponse<bool>.Success(result.Succeeded);
        }

        public async Task<ApiResponse<bool>> ChangeNameAsync(string userId, ChangeNameDto changeNameDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<bool>.Fail(HttpStatusCode.NotFound, errors: "User is not found");

            user.Name = changeNameDto.NewName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ApiResponse<bool>.Fail(errors: result.Errors.Select(t => t.Description).ToArray());

            return ApiResponse<bool>.Success(result.Succeeded);
        }
    }
}