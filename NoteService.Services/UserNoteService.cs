using System.Text.Json;
using AutoMapper;
using NoteService.Domain.Models;
using NoteService.Domain.OutboxModel;
using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;
using NoteService.Shared.DataTransferObjects;
using NoteService.Shared.Events;

namespace NoteService.Services
{
    internal class UserNoteService : IUserNoteService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        private readonly IOutboxRepository _outboxRepository;
        public UserNoteService(IRepositoryManager repositoryManager, IMapper mapper,
                               IOutboxRepository outboxRepository)
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _outboxRepository = outboxRepository;
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
            if (userNote is null)
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

        public async Task ConvertToPdf(UserNoteForConversion note)
        {
            var userNote = await _repository.UserNoteRepository.GetNoteByNoteIdAsync(note.NoteID, false);
            if (userNote is null) return;
            if (userNote.UserID != note.UserID) return;
            ConversionJob conversionJob = new ConversionJob()
            {
                Format = "Pdf",
                Password = note.Password,
                UserEmail = note.UserEmail,
                Note = new Note()
                {
                    Title = userNote.Title,
                    Content = userNote.Content
                }
            };
            var outboxMessage = new OutboxMessage
    {
        Id = Guid.NewGuid(),
        EventType = nameof(ConversionJob),
        Content = JsonSerializer.Serialize(conversionJob),
        CreatedAt = DateTime.UtcNow
    };
            await _outboxRepository.AddAsync(outboxMessage);

        }
    }
}
