using NoteService.Shared.DataTransferObjects;

namespace NoteService.Services.Abstraction
{
    public interface IUserNoteService
    {
        IEnumerable<UserNoteDto> GetNotesByUserId(string userId, bool trackChanges);

        void CreateNote(UserNoteDto note);
    }
}
