using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            var db = scope.ServiceProvider.GetRequiredService<OutboxContext>();

            var messages = await db.OutboxMessages
                .Where(x => !x.IsProcessed)
                .Take(10)
                .ToListAsync();

            foreach (var msg in messages)
            {
                try
                {
                    var job = JsonSerializer.Deserialize<ConversionJob>(msg.Content);
                    await _queue.PublishAsync("convert-jobs", job);

                    msg.IsProcessed = true;
                    msg.ProcessedAt = DateTime.UtcNow;

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // log error and retry in next cycle
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
