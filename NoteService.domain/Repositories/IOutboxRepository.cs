using NoteService.Domain.OutboxModel;
using NoteService.Shared.DataTransferObjects;

namespace NoteService.Domain.Repositories;

public interface IOutboxRepository
{
    Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(int maxBatchSize);
    Task MarkAsProcessedAsync(Guid messageId);
    Task AddAsync(OutboxMessage message);

    Task IncrementRetryOrDeadLetterAsync(Guid messageId, Exception ex);
}