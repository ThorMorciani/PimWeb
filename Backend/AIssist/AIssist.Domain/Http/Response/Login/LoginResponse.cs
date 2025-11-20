namespace AIssist.Domain.Http.Response.Login
{
    public class LoginResponse
    {
        public required string RefreshToken { get; set; }
        public required string AccessToken { get; set; }
        public required string Username { get; set; }
        public required UserDto User { get; set; }
    }
}

