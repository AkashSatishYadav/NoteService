using AutoMapper;
using NoteService.Domain.Models;
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

        public async Task CreateNoteAsync(UserNoteDto note)
        {
            var userNote = _mapper.Map<UserNote>(note);
            var time = DateTime.UtcNow;
            userNote.CreatedAt = time;
            userNote.UpdatedAt = time;
            _repository.UserNoteRepository.CreateNote(userNote);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<UserNoteDto>> GetNotesByUserIdAsync(string userId, bool trackChanges)
        {
            var userNotes = await _repository.UserNoteRepository.GetNotesByUserIdAsync(userId, trackChanges);

            var userNotesDto = _mapper.Map<IEnumerable<UserNoteDto>>(userNotes);

            return userNotesDto;
        }

        public async Task UpdateNoteAsync(UserNoteDto note)
        {
            var userNote = await _repository.UserNoteRepository.GetNoteByNoteIdAsync(note.NoteID, true);
            if(userNote is null)
                return;

            _mapper.Map(note, userNote);
            var time = DateTime.UtcNow;
            userNote.UpdatedAt = time;
            await _repository.SaveAsync();
        }

        public async Task DeleteNoteAsync(UserNoteForDeleteDto note)
        {
            var userNote = await _repository.UserNoteRepository.GetNoteByNoteIdAsync(note.NoteID, true);
            if (userNote is null)
                return;

            userNote.IsDeleted = true;
            var time = DateTime.UtcNow;
            userNote.UpdatedAt = time;
            await _repository.SaveAsync();
        }
    }
}
