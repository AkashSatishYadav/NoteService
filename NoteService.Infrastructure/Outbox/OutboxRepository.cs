using Microsoft.EntityFrameworkCore;
using NoteService.Domain.OutboxModel;
using NoteService.Domain.Repositories;
using NoteService.Shared.DataTransferObjects;

namespace NoteService.Infrastructure.Outbox;

public class OutboxRepository : IOutboxRepository
{
    private const int MaxRetries = 5;
    private readonly OutboxContext _outboxContext;
    public OutboxRepository(OutboxContext outboxContext)
    {
        _outboxContext = outboxContext;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        _outboxContext.OutboxMessages!.Add(message);
        await _outboxContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(int maxBatchSize)
    {
        return await _outboxContext.OutboxMessages!
                .Where(x => !x.IsProcessed && !x.IsDeadLettered)
                .OrderBy(x => x.CreatedAt)
                .Take(maxBatchSize)
                .AsNoTracking()
                .ToListAsync();

    }

    public async Task MarkAsProcessedAsync(Guid messageId)
    {
        var message = await _outboxContext.OutboxMessages!.FindAsync(messageId);
        if (message != null)
        {
            message.IsProcessed = true;
            message.ProcessedAt = DateTime.UtcNow;
            await _outboxContext.SaveChangesAsync();
        }
    }
    
    public async Task IncrementRetryOrDeadLetterAsync(Guid messageId, Exception ex)
    {
    var message = await _outboxContext.OutboxMessages!.FindAsync(messageId);
    if (message != null)
    {
        message.RetryCount++;

        if (message.RetryCount >= MaxRetries)
        {
            message.IsDeadLettered = true;
            // Optionally log or store the exception details
            Console.WriteLine($"Message {messageId} moved to DLQ after {message.RetryCount} retries: {ex.Message}");
        }

        await _outboxContext.SaveChangesAsync();
    }
    }

}
