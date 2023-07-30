using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerX.AuthApi.Data;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.HostBuilders
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
