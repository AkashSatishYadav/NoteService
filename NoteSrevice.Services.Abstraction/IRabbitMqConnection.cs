using RabbitMQ.Client;

namespace NoteService.Services.Abstraction
{
    public interface IRabbitMqConnection
    {
       IChannel Channel { get; }
    }
}
