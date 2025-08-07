using System.ComponentModel.DataAnnotations;

namespace NoteService.Shared.DataTransferObjects
{
    public class UserNoteDto
    {
        public Guid NoteID { get; set; }

        [Required]
        [MaxLength(100)] // or [StringLength(100)]
        public string UserID { get; set; }

        [Required]
        [MaxLength(150)] // reasonable limit for a title
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)] // limit to 10k chars or whatever fits your app
        public string Content { get; set; }
    }
}
