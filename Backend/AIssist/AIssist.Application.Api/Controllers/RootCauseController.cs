using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Http.Request.RootCause;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RootCauseController : Controller
    {
        private readonly IRootCauseAppService _rootCauseAppService;

        public RootCauseController(IRootCauseAppService rootCauseAppService)
        {
            _rootCauseAppService = rootCauseAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _rootCauseAppService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RootCausePostRequest rootCauseRequest)
        {
            await _rootCauseAppService.Add(rootCauseRequest);

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _rootCauseAppService.Get();

            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(RootCausePutRequest rootCauseRequest)
        {
            await _rootCauseAppService.Update(rootCauseRequest);

            return Ok();
        }

        [HttpDelete("{rootCauseId}")]
        public async Task<IActionResult> Inactivate(long rootCauseId)
        {
            await _rootCauseAppService.Inactivate(rootCauseId);

            return Ok();
        }
    }
}

