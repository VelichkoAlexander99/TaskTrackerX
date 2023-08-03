using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TaskTrackerX.AuthApi.Models.Options;

namespace TaskTrackerX.AuthApi.Services.Publisher.Factory
{
    public class RabbitMqPublisherFactory : IRabbitMqPublisherFactory
    {
        private readonly IConnection _connection;
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbitMqPublisherFactory(
            IOptions<SettingOptions> options)
        {
            _rabbitMqConfig = options.Value.RabbitMqConfig;
            var factory = new ConnectionFactory { Uri = new Uri(_rabbitMqConfig.ConnectionString) };
            _connection = factory.CreateConnection();
        }

        public void PublisherUserDelete(Guid userId)
        {
            using (var rabbitMqManager = new RabbitMqPublisher<Guid>(
                _connection, 
                _rabbitMqConfig.QueueAuthUserDelete))
            {
                rabbitMqManager.Publish(userId);
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
