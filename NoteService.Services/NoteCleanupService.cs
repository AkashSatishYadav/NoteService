using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;

namespace NoteService.Services
{
    public class NoteCleanupService : INoteCleanupService
    {
        private readonly IRepositoryManager _repo;

        public NoteCleanupService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task CleanupOldDeletedNotesAsync()
        {
            var cutoff = DateTime.UtcNow.AddMonths(-1);
            var notes = await _repo.UserNoteRepository.GetOldDeletedNotesAsync(cutoff);

            foreach (var note in notes)
            {
                _repo.UserNoteRepository.DeleteNote(note);
            }

            await _repo.SaveAsync();
        }
    }
}
