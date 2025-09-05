using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AIssist.Application.Api.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AIssist.Application.Api.Services
{
	public class AuthService : IAuthService
	{
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IUserService userService)
		{
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userService.GetByUsername(request.Username);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<Users>().VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateTokenResponse(user);
        }

        private LoginResponse CreateTokenResponse(Users? user)
        {
            return new LoginResponse
            {
                User = user,
                AccessToken = CreateToken(user)
            };
        }

        private string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                //new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("token:key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("token:issuer"),
                audience: _configuration.GetValue<string>("token:audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}

