using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Repositories;
using NoteService.Shared.DataTransferObjects;

namespace NoteService.Infrastructure.Outbox;

public class OutboxRepository :  IOutboxRepository
{
    private readonly OutboxContext _outboxContext;
    public OutboxRepository(OutboxContext outboxContext)
    {
        _outboxContext = outboxContext;
    }

    public async Task AddAsync(OutboxMessageDto message)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OutboxMessageDto>> GetUnprocessedMessagesAsync(int maxBatchSize)
    {
        var outboxMessages = await _outboxContext.OutboxMessages!
                .Where(x => !x.IsProcessed)
                .OrderBy(x => x.CreatedAt)
                .Take(maxBatchSize)
                .ToListAsync();

    }

    public async Task MarkAsProcessedAsync(Guid messageId)
    {
        throw new NotImplementedException();
    }

}
