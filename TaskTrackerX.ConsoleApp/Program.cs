using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; // Requires NuGet package
using Microsoft.Extensions.Options;
using TaskTrackerX.Database;

Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


        void configureDbContext(DbContextOptionsBuilder o) => 
            o.UseNpgsql("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=ny1CaIxh4");

        services.AddDbContext<ApplicationDbContext>(configureDbContext);
    })
    .Build()
    .Run();