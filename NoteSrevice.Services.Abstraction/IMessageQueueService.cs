namespace NoteService.Services.Abstraction
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string queueName, T message);
    }
}
