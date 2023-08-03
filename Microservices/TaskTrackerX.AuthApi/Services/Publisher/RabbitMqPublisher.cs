using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace TaskTrackerX.AuthApi.Services.Publisher
{
    public class RabbitMqPublisher<T> : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public RabbitMqPublisher(IConnection connection, string exchangeName)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;

            _channel.ExchangeDeclare(
                exchange: _exchangeName,
                type: ExchangeType.Direct);
        }

        public void Publish(T message)
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: "",
                basicProperties: null,
                body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
