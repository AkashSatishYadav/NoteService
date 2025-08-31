using System.ComponentModel.DataAnnotations;

namespace NoteService.Infrastructure.Outbox;

[System.ComponentModel.DataAnnotations.Schema.Table("OutboxMessages")]
    public class OutboxMessage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string EventType { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; }

        public bool IsProcessed { get; set; } = false;
    }

