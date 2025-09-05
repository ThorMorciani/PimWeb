using System;
using AIssist.Domain.Entities;

namespace AIssist.Domain.Http.Response
{
	public class LoginResponse
	{
		public required Users User { get; set; }
        public required string AccessToken { get; set; }
    }
}

