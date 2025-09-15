using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;
using NoteService.Shared.Events;

namespace NoteService.Infrastructure.Outbox;

public class OutboxDispatcherService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMessageQueueService _queue;

    public OutboxDispatcherService(IServiceScopeFactory scopeFactory, IMessageQueueService queue)
    {
        _scopeFactory = scopeFactory;
        _queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

            var messages = await outboxRepository.GetUnprocessedMessagesAsync(50);

            foreach (var msg in messages)
            {
                try
                {
                    var job = JsonSerializer.Deserialize<ConversionJob>(msg.Content);
                    await _queue.PublishAsync("convert-jobs", job);

                    await outboxRepository.MarkAsProcessedAsync(msg.Id);
                }
                catch (Exception ex)
                {
                    await outboxRepository.IncrementRetryOrDeadLetterAsync(msg.Id, ex);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
