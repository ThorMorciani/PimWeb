namespace AIssist.Domain.Http.Request.Functionality
{
    public class FunctionalityRequest
    {
        public long Id { get; set; }
        public string? Functionality { get; set; }
        public string? CreatedBy { get; set; }
        public long ProfileId { get; set; }
    }
}

