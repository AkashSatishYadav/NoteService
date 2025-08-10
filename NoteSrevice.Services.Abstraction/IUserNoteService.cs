using NoteService.Shared.DataTransferObjects;

namespace NoteService.Services.Abstraction
{
    public interface IUserNoteService
    {
        Task<IEnumerable<UserNoteDto>> GetNotesByUserIdAsync(string userId, bool trackChanges);

        Task CreateNoteAsync(UserNoteDto note);

        Task UpdateNoteAsync(UserNoteDto note);

        Task DeleteNoteAsync(UserNoteForDeleteDto note);
    }
}
