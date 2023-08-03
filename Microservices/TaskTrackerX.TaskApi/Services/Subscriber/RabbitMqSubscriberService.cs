using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskTrackerX.TaskApi.Data.Stores.ExerciseStore;
using TaskTrackerX.TaskApi.HostBuilders;
using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Models.Options;

namespace TaskTrackerX.TaskApi.Services.Subscriber
{
    public class RabbitMqSubscriberService : IHostedService
    {
        private readonly RabbitMqSubscriber<Guid> _subscriber;

        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<SettingOptions> _options;

        public RabbitMqSubscriberService(
            IServiceProvider serviceProvider,
            IOptions<SettingOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
            _subscriber = new RabbitMqSubscriber<Guid>(options.Value.RabbitMqConfig.ConnectionString);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(_options.Value.RabbitMqConfig.QueueAuthUserDelete, OnAuthApiDeleteUser);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }

        private async void OnAuthApiDeleteUser(Guid userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var exerciseStore = scope.ServiceProvider.GetRequiredService<IExerciseStore>();
                await exerciseStore.ArchiveByUserId(userId);
            }
        }
    }
}
