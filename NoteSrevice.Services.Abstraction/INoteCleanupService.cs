namespace NoteService.Services.Abstraction
{
    public interface INoteCleanupService
    {
        Task CleanupOldDeletedNotesAsync();
    }
}
