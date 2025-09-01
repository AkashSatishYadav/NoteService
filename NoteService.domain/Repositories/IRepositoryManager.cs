namespace NoteService.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserNoteRepository UserNoteRepository { get; }

        IOutboxRepository OutboxRepository { get; }

        Task SaveAsync();
    }
}
