using System;
namespace AIssist.Domain.Http.Request.User
{
	public class UserPutRequest
	{
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public long ProfileId { get; set; }
    }
}

