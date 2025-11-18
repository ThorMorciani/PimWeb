using AIssist.Domain.Enums;

namespace AIssist.Domain.Http.Response.RootCause
{
	public class RootCauseResponse
	{
        public long Id { get; set; }
        public string RootCauseName { get; set; } = string.Empty;
        public TicketCriticality Criticality { get; set; }
    }
}

