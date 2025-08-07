using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Models;

namespace NoteService.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserNote>? UserNotes { get; set; }
    }
}
