namespace AIssist.Domain.Http.Response.Login
{
    public class LoginResponse
    {
        public required string RefreshToken { get; set; }
        public required string AccessToken { get; set; }
        public required string Username { get; set; }
        public required long Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Profile { get; set; }
    }
}

