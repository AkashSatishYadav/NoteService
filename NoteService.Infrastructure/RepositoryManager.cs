using NoteService.Domain.Repositories;
using NoteService.Infrastructure.Repositories;

namespace NoteService.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        public IUserNoteRepository UserNoteRepository => _userNoteRepository.Value;

        public IOutboxRepository OutboxRepository => throw new NotImplementedException();

        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserNoteRepository> _userNoteRepository;

        private readonly Lazy<IOutboxRepository> _outboxRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userNoteRepository = new Lazy<IUserNoteRepository>(() => new UserNoteRepository(repositoryContext));
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
