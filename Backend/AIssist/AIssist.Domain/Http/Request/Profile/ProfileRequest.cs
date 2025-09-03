namespace AIssist.Domain.Http.Request.Profile
{
    public class ProfileRequest
    {
        public long Id { get; set; }
        public string Profile { get; set; }
        public List<string> Permissions { get; set; }
    }
}

