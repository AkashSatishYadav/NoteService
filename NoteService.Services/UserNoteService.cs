using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;

namespace NoteService.Services
{
    internal class UserNoteService : IUserNoteService
    {
        private readonly IRepositoryManager _repository;
        public UserNoteService(IRepositoryManager repositoryManager) 
        {
            _repository = repositoryManager;       
        }    
    }
}
