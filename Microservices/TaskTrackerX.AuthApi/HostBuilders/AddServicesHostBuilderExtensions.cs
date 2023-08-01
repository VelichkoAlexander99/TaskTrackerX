using TaskTrackerX.AuthApi.Services;
using TaskTrackerX.AuthApi.Services.JwtTokenGenerator;

namespace TaskTrackerX.AuthApi.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<UserWithRoleService>();
        }
    }
}
