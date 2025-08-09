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

        public UserNote GetNoteByNoteId(Guid nodeId, bool trackChanges) =>
            FindByCondition(n => n.NoteID == nodeId, trackChanges).SingleOrDefault();

        public IEnumerable<UserNote> GetNotesByUserId(string userId, bool trackChanges) =>
            FindByCondition(n => n.UserID == userId && !n.IsDeleted, trackChanges).OrderByDescending(n => n.UpdatedAt).ToList();
    }
}
