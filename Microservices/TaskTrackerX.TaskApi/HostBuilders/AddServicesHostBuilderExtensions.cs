using TaskTrackerX.TaskApi.Data.Stores.ExerciseStore;
using TaskTrackerX.TaskApi.Data.Stores.StatusStore;
using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Services;
using TaskTrackerX.TaskApi.Services.UserService;
using TaskTrackerX.TaskApi.Validator.ExerciseValidator;
using TaskTrackerX.TaskApi.Validator.StatusValidator;

namespace TaskTrackerX.TaskApi.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<BearerTokenHandler>();

            services.AddScoped<IStatusStore, StatusStore>();
            services.AddScoped<IExerciseStore, ExerciseStore>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IStatusManager, StatusManager>();
            services.AddScoped<IExerciseManager, ExerciseManager>();

            services.AddScoped<IStatusValidator, StatusValidator>();
            services.AddScoped<IExerciseValidator, ExerciseValidator>();

        }
    }
}
