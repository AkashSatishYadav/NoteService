using NoteService.Domain.Models;

namespace NoteService.Domain.Repositories
{
    public interface IUserNoteRepository
    {
        IEnumerable<UserNote> GetNotesByUserId(string userId, bool trackChanges);

        UserNote GetNoteByNoteId(Guid nodeId, bool trackChanges);
        void CreateNote(UserNote note);

    }
}
