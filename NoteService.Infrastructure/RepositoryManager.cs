using NoteService.Domain.Repositories;
using NoteService.Infrastructure.Repositories;

namespace NoteService.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        public IUserNoteRepository UserNoteRepository => _userNoteRepository.Value;

        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserNoteRepository> _userNoteRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userNoteRepository = new Lazy<IUserNoteRepository>(() => new UserNoteRepository(repositoryContext));
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
