namespace NoteService.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserNoteRepository UserNoteRepository { get; }

        void Save();
    }
}
