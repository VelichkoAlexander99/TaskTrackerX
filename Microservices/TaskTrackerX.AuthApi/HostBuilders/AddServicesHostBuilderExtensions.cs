using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskTrackerX.AuthApi.Models;
using TaskTrackerX.AuthApi.Services.AuthService;
using TaskTrackerX.AuthApi.Services.JwtTokenGenerator;
using TaskTrackerX.AuthApi.Services.RoleService;
using TaskTrackerX.AuthApi.Services.UserService;

namespace TaskTrackerX.AuthApi.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
