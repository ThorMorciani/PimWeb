using AIssist.Domain.Enums;

namespace AIssist.Domain.Entities
{
    public class Tickets
    {
        public long Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Solution { get; set; } = string.Empty;

        public string TicketNumber { get; set; } = string.Empty;

        public long? AssigneeId { get; set; } = null;

        public long ReporterId { get; set; }

        public long RootCauseId { get; set; }

        public TicketStatus Status { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Users? Assignee { get; set; }

        public Users? Reporter { get; set; }

        public RootCause? RootCause { get; set; }
    }
}

