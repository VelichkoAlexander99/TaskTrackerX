using TaskTrackerX.AuthApi.Services;
using TaskTrackerX.AuthApi.Services.JwtTokenGenerator;
using TaskTrackerX.AuthApi.Services.Publisher.Factory;

namespace TaskTrackerX.AuthApi.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRabbitMqPublisherFactory, RabbitMqPublisherFactory>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<UserWithRoleService>();
        }
    }
}
