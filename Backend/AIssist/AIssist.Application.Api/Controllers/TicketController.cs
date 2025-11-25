using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Http.Request.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AIssist.Application.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketAppService _ticketAppService;

        public TicketController(ITicketAppService ticketAppService)
        {
            _ticketAppService = ticketAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketPostRequest ticketPostRequest)
        {
            try
            {
                var result = await _ticketAppService.Add(ticketPostRequest);

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

        [HttpGet("{ticketNumber}")]
        public async Task<IActionResult> GetByTicketNumber(string ticketNumber)
        {
            try
            {
                var result = await _ticketAppService.GetByTicketNumber(ticketNumber);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("/byReporter/{reporterId}")]
        public async Task<IActionResult> GetByReporterId(long reporterId)
        {
            try
            {
                var result = await _ticketAppService.GetByReporterId(reporterId);

                return Ok(result);
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
                var result = await _ticketAppService.Get();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update(TicketPutRequest ticketRequest)
        {
            try
            {
                var result = await _ticketAppService.Update(ticketRequest);

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

        [HttpPut("/status")]
        public async Task<IActionResult> UpdateStatus(TicketPutStatusRequest ticketRequest)
        {
            try
            {
                var result = await _ticketAppService.UpdateStatus(ticketRequest.TicketNumber, ticketRequest.Status);

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

        [HttpPut("/assignee")]
        public async Task<IActionResult> UpdateAssignee(TicketPutAssigneeRequest ticketRequest)
        {
            try
            {
                var result = await _ticketAppService.UpdateAssignee(ticketRequest.TicketNumber, ticketRequest.AssigneeId);

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
    }
}

