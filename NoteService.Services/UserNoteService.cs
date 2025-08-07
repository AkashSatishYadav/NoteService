using AutoMapper;
using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;
using NoteService.Shared.DataTransferObjects;

namespace NoteService.Services
{
    internal class UserNoteService : IUserNoteService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public UserNoteService(IRepositoryManager repositoryManager, IMapper mapper) 
        {
            _repository = repositoryManager;  
            _mapper = mapper;
        }

        public IEnumerable<UserNoteDto> GetNotesByUserId(string userId, bool trackChanges)
        {
            var userNotes = _repository.UserNoteRepository.GetNotesByUserId(userId, trackChanges);

            var userNotesDto = _mapper.Map<IEnumerable<UserNoteDto>>(userNotes);

            return userNotesDto;
        }
    }
}
