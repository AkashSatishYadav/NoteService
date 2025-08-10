using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Models;
using NoteService.Domain.Repositories;

namespace NoteService.Infrastructure.Repositories
{
    internal class UserNoteRepository : RepositoryBase<UserNote>, IUserNoteRepository
    {
        public UserNoteRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNote(UserNote note) =>
            Create(note);

        public async Task<UserNote> GetNoteByNoteIdAsync(Guid nodeId, bool trackChanges) =>
           await FindByCondition(n => n.NoteID == nodeId, trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<UserNote>> GetNotesByUserIdAsync(string userId, bool trackChanges) =>
           await FindByCondition(n => n.UserID == userId && !n.IsDeleted, trackChanges).OrderByDescending(n => n.UpdatedAt).ToListAsync();
    }
}
