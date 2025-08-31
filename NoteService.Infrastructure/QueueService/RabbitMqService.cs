using NoteService.Services.Abstraction;
using System.Text.Json;
using RabbitMQ.Client;

namespace NoteService.Infrastructure.QueueService
{
    public class RabbitMqService : IMessageQueueService
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;

        public RabbitMqService(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            var props = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent // Persistent message
            };
            await using var channel = await _rabbitMqConnection.Connection.CreateChannelAsync();
            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                mandatory: false,
                basicProperties: props,
                body: body);
        }
    }
}
