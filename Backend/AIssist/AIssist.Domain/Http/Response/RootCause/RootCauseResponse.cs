using AIssist.Domain.Enums;

namespace AIssist.Domain.Http.Response.RootCause
{
	public class RootCauseResponse
	{
        public long Id { get; set; }
        public string RootCauseName { get; set; } = string.Empty;
        public TicketCriticality Criticality { get; set; }
        public string UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}

