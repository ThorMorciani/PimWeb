using AIssist.Application.Services.Interfaces;
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
    }
}

