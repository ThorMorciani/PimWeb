namespace AIssist.Domain.Http.Request.Criticality
{
    public class CriticalityRequest
    {
        public long Id { get; set; }
        public string? Criticality { get; set; }
        public string? CreatedBy { get; set; }
    }
}

