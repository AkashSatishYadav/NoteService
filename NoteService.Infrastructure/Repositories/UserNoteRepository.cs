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

        public IEnumerable<UserNote> GetNotesByUserId(string userId, bool trackChanges) =>
            FindByCondition(n => n.UserID == userId, trackChanges).OrderByDescending(n => n.UpdatedAt).ToList();
    }
}
