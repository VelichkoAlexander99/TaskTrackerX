using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTrackerX.AuthApi.Data;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.HostBuilders
{
    public static class AddIdentityHostBuilderExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
            })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
        }
    }
}
