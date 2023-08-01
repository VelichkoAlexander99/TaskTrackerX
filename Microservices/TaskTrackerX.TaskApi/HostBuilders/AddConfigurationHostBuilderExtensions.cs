using TaskTrackerX.TaskApi.Models.Options;

namespace TaskTrackerX.TaskApi.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<SettingOptions>()
                    .Bind(configuration.GetSection("SettingOptions"))
                    .ValidateDataAnnotations();

            services.Configure<SettingOptions>(
                configuration.GetSection("SettingOptions"));
        }
    }
}
