using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Http.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _userAppService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserPostRequest userRequest)
        {
            await _userAppService.Add(userRequest);

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _userAppService.Get();

            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UserPutRequest userRequest)
        {
            await _userAppService.Update(userRequest);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Inactivate(long userId)
        {
            await _userAppService.Inactivate(userId);

            return Ok();
        }
    }
}

