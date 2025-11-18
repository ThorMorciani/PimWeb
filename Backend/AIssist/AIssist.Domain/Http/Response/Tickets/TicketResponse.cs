using AIssist.Domain.Http.Response.RootCause;
using AIssist.Domain.Http.Response.Users;

namespace AIssist.Domain.Http.Response.Tickets
{
	public class TicketResponse
	{
		public long Id { get; set; }
		public string? Description { get; set; }
		public string? Solution { get; set; }
		public string? TicketNumber { get; set; }
		public string? Status { get; set; }
		public long StatusId { get; set; }
        public long? AssigneeId { get; set; }
        public long ReporterId { get; set; }
        public long RootCauseId { get; set; }
        public RootCauseResponse RootCause { get; set; }
		public UserResponse Reporter { get; set; }
        public UserResponse? Assignee { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

