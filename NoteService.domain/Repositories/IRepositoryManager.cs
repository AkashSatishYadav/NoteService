namespace NoteService.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserNoteRepository UserNoteRepository { get; }

        Task SaveAsync();
    }
}
