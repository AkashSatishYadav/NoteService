using System.ComponentModel.DataAnnotations;

namespace NoteService.Shared.DataTransferObjects
{
    public class UserNoteForDeleteDto
    {
        [Required]
        public Guid NoteID { get; set; }
    }
}
