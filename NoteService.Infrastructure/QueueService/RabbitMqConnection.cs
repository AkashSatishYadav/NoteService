using NoteService.Services.Abstraction;
using RabbitMQ.Client;

namespace NoteService.Infrastructure.QueueService
{
    public class RabbitMqConnection : IDisposable, IRabbitMqConnection
    {
        private IConnection _connection;
        private IChannel? _channel;
        private const string MainQueue = "convert-jobs";
        public IChannel Channel => _channel!;

        public async Task InitializeConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            _connection = await factory.CreateConnectionAsync();

            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(queue: MainQueue, false, false, false, null);
        }
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
