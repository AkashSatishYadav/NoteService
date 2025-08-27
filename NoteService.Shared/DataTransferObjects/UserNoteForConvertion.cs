using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NoteService.Shared.DataTransferObjects
{
    public class UserNoteForConversion
    {
        [Required]
        public Guid NoteID { get; set; }

        [Required]
        [MaxLength(100)] // or [StringLength(100)]
        public string UserID { get; set; }

        public string Password { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }
}
