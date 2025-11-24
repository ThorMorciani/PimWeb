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
            try
            {
                var result = await _userAppService.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserPostRequest userRequest)
        {
            try
            {
                var result = await _userAppService.Add(userRequest);
                if (result.Success)
                    return Ok();
                else
                    return BadRequest(new { result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _userAppService.Get();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UserPutRequest userRequest)
        {
            try
            {
                var result = await _userAppService.Update(userRequest);

                if (result.Success)
                    return Ok();
                else
                    return BadRequest(new { result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Inactivate(long userId)
        {
            try
            {
                var result = await _userAppService.Inactivate(userId);

                if (result.Success)
                    return Ok();
                else
                    return BadRequest(new { result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("technicians")]
        public async Task<IActionResult> GetTechnicians()
        {
            var result = await _userAppService.GetTechnicians();
            return Ok(result);
        }

    }
}

