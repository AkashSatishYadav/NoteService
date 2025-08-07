namespace NoteService.Services.Abstraction
{
    public interface IServiceManager
    {
        IUserNoteService UserNoteService { get; }
    }
}
