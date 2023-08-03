using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace TaskTrackerX.TaskApi.Services.Subscriber
{
    public class RabbitMqSubscriber<T> : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqSubscriber(string connectionString)
        {
            var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Subscribe(string exchange, Action<T> messageHandler)
        {
            _channel.ExchangeDeclare(
                exchange: exchange,
                type: ExchangeType.Direct);

            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: queueName,
                exchange: exchange,
                routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var deserializedMessage = JsonConvert.DeserializeObject<T>(message);
                messageHandler?.Invoke(deserializedMessage);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
