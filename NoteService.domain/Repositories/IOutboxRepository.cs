using NoteService.Shared.DataTransferObjects;

namespace NoteService.Domain.Repositories;

 public interface IOutboxRepository
    {
        Task<IEnumerable<OutboxMessageDto>> GetUnprocessedMessagesAsync(int maxBatchSize);
        Task MarkAsProcessedAsync(Guid messageId);
        Task AddAsync(OutboxMessageDto message);
    }