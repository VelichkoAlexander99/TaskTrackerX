using Microsoft.Extensions.Hosting;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtSettings>()
                    .Bind(configuration.GetSection("JwtSettings"))
                    .ValidateDataAnnotations();

            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));
        }
    }
}
