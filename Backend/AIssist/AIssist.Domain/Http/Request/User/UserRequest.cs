namespace AIssist.Domain.Http.Request.User
{
    public class UserRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public long profileId { get; set; }
    }
}

