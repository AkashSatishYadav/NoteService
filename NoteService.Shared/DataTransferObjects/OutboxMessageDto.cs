namespace NoteService.Shared.DataTransferObjects;

public class OutboxMessageDto
    {
        public Guid Id { get; set; }

        public string EventType { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; }

        public bool IsProcessed { get; set; } = false;
    }
