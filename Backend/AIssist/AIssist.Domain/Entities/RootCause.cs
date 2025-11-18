using System.ComponentModel.DataAnnotations.Schema;
using AIssist.Domain.Enums;

namespace AIssist.Domain.Entities
{
    public class RootCause
    {
        public long Id { get; set; }

        [Column("RootCause")]
        public string RootCauseName { get; set; } = string.Empty;

        public TicketCriticality Criticality { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; } = string.Empty;

        public bool Active { get; set; }

        public ICollection<Tickets>? Tickets { get; set; }
    }
}

