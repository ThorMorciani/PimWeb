namespace AIssist.Domain.Http.Request.RootCause
{
	public class RootCausePutRequest
	{
        public long Id { get; set; }
        public string? RootCause { get; set; }
        public long CriticalityId { get; set; }
        public string? CreatedBy { get; set; }
    }
}

