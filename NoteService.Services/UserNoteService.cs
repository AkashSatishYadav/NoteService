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

        public void CreateNote(UserNoteDto note)
        {
            var userNote = _mapper.Map<UserNote>(note);
            var time = DateTime.UtcNow;
            userNote.CreatedAt = time;
            userNote.UpdatedAt = time;
            _repository.UserNoteRepository.CreateNote(userNote);
            _repository.Save();
        }

        public IEnumerable<UserNoteDto> GetNotesByUserId(string userId, bool trackChanges)
        {
            var userNotes = _repository.UserNoteRepository.GetNotesByUserId(userId, trackChanges);

            var userNotesDto = _mapper.Map<IEnumerable<UserNoteDto>>(userNotes);

            return userNotesDto;
        }

        public void UpdateNote(UserNoteDto note)
        {
            var userNote = _repository.UserNoteRepository.GetNoteByNoteId(note.NoteID, true);
            if(userNote is null)
                return;

            _mapper.Map(note, userNote);
            var time = DateTime.UtcNow;
            userNote.UpdatedAt = time;
            _repository.Save();


        }

        public void DeleteNote(UserNoteForDeleteDto note)
        {
            var userNote = _repository.UserNoteRepository.GetNoteByNoteId(note.NoteID, true);
            if (userNote is null)
                return;

            userNote.IsDeleted = true;
            var time = DateTime.UtcNow;
            userNote.UpdatedAt = time;
            _repository.Save();
        }
    }
}
