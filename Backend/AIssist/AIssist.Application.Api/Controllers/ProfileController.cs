using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Http.Request.Profile;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileController(IProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _profileAppService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfileRequest profileRequest)
        {
            await _profileAppService.Add(profileRequest);

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _profileAppService.Get();

            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(ProfileRequest profileRequest)
        {
            await _profileAppService.Update(profileRequest);

            return Ok();
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> Inactivate(long profileId)
        {
            await _profileAppService.Inactivate(profileId);

            return Ok();
        }
    }
}

