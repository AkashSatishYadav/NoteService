using NoteService.Domain.Models;

namespace NoteService.Domain.Repositories
{
    public interface IUserNoteRepository
    {
        Task<IEnumerable<UserNote>> GetNotesByUserIdAsync(string userId, bool trackChanges);

        Task<UserNote> GetNoteByNoteIdAsync(Guid nodeId, bool trackChanges);
        void CreateNote(UserNote note);

    }
}
