using Microsoft.EntityFrameworkCore;
using TaskTrackerX.TaskApi.Data;

namespace TaskTrackerX.TaskApi.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
