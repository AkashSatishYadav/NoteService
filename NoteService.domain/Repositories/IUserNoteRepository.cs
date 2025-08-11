using NoteService.Domain.Models;

namespace NoteService.Domain.Repositories
{
    public interface IUserNoteRepository
    {
        Task<IEnumerable<UserNote>> GetNotesByUserIdAsync(string userId, bool trackChanges);
        Task<UserNote> GetNoteByNoteIdAsync(Guid nodeId, bool trackChanges);
        void CreateNote(UserNote note);
        Task<IEnumerable<UserNote>> GetOldDeletedNotesAsync(DateTime cutoffDate);
        void DeleteNote(UserNote note);
    }
}
