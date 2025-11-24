using RabbitMQ.Client;

namespace NoteService.Services.Abstraction
{
    public interface IRabbitMqConnection
    {
       IConnection Connection { get; }
    }
}
