using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIssist.Domain.Http.Response.Login
{
    public class UserDto
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string ProfileName { get; set; }
    }
}

