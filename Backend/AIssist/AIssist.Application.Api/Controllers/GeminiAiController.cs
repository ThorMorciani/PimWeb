using AIssist.Application.Services;
using AIssist.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class GeminiAiController : ControllerBase
    {
        private readonly IAService _aiService;

        public GeminiAiController(IAService aiService)
        {
            _aiService = aiService;
        }

        public class GeminiSuggestionRequest
        {
            public string Description { get; set; } = "";
        }

        public class GeminiSuggestionResponse
        {
            public string OutputText { get; set; } = "";
        }

        public class GeminiPriorityResponse
        {
            public string Priority { get; set; } = "";
        }

        [HttpPost("suggestion")]
        public async Task<IActionResult> Suggestion([FromBody] GeminiSuggestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
                return BadRequest("Descrição não pode ser vazia.");

            var suggestion = await _aiService.GetSuggestion(request.Description);
            return Ok(new GeminiSuggestionResponse { OutputText = suggestion });
        }

        [HttpPost("priority")]
        public async Task<IActionResult> Priority([FromBody] GeminiSuggestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
                return BadRequest("Descrição não pode ser vazia.");

            var isHighPriority = await _aiService.AnalyzePriority(request.Description);
            return Ok(new GeminiPriorityResponse
            {
                Priority = isHighPriority ? "Alta" : "Normal"
            });
        }
    }
}
