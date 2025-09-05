using AIssist.Application.Api.Services.Interfaces;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }
    }
}

