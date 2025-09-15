using Microsoft.EntityFrameworkCore;
using NoteService.Domain.OutboxModel;

namespace NoteService.Infrastructure.Outbox;

public class OutboxContext : DbContext
{
    public OutboxContext(DbContextOptions<OutboxContext> options) : base(options)
    {

    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;
}
