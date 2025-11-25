using System.Text.Json;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Ticket;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Http.Response.Tickets;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;

namespace AIssist.Application.Services
{
    public class TicketAppService : ITicketAppService
    {
        private readonly ITicketService _ticketService;
        private readonly ILogAppService _logAppService;
        private readonly IMapper _mapper;

        public TicketAppService(
            ITicketService ticketService,
            ILogAppService logAppService,
            IMapper mapper)
        {
            _ticketService = ticketService;
            _logAppService = logAppService;
            _mapper = mapper;
        }

        public async Task<DefaultResponse> Add(TicketPostRequest entity)
        {
            var response = new DefaultResponse();
            var ticket = _mapper.Map<Tickets>(entity);

            var result = await _ticketService.Add(ticket);

            if (result)
            {
                await _logAppService.Add("Inserção", JsonSerializer.Serialize(entity));
                response.Success = true;
            }
            else
            {
                response.Message = "Falha ao salvar registro.";
            }

            return response;
        }

        public async Task<List<TicketResponse>> Get()
        {
            var result = await _ticketService.Get();
            var mapped = _mapper.Map<List<TicketResponse>>(result);
            return mapped;
        }

        public async Task<TicketResponse?> GetByTicketNumber(string ticketNumber)
        {
            var result = await _ticketService.GetByTicketNumber(ticketNumber);
            var mapped = _mapper.Map<TicketResponse>(result);
            return mapped;
        }

        public async Task<List<TicketResponse>> GetByReporterId(long reporterId)
        {
            var result = await _ticketService.GetByReporterId(reporterId);
            var mapped = _mapper.Map<List<TicketResponse>>(result);
            return mapped;
        }

        public async Task<DefaultResponse> Update(TicketPutRequest entity)
        {
            var response = new DefaultResponse();
            var ticket = _mapper.Map<Tickets>(entity);

            var result = await _ticketService.Update(ticket);

            if (result)
            {
                await _logAppService.Add("Atualização", JsonSerializer.Serialize(entity));
                response.Success = true;
            }
            else
            {
                response.Message = "Falha ao atualizar registro.";
            }

            return response;
        }

        public async Task<DefaultResponse> UpdateStatus(string ticketNumber, long newStatus)
        {
            var response = new DefaultResponse();
            var result = await _ticketService.UpdateStatus(ticketNumber, newStatus);

            if (result)
            {
                await _logAppService.Add(
                    "Atualização Status",
                    JsonSerializer.Serialize(new { ticketNumber, Status = newStatus })
                );

                response.Success = true;
            }
            else
            {
                response.Message = "Falha ao atualizar registro.";
            }

            return response;
        }

        public async Task<DefaultResponse> UpdateAssignee(string ticketNumber, long assigneeId)
        {
            var response = new DefaultResponse();
            var result = await _ticketService.UpdateAssignee(ticketNumber, assigneeId);

            if (result)
            {
                await _logAppService.Add(
                    "Atualização Assignee",
                    JsonSerializer.Serialize(new { ticketNumber, Assignee = assigneeId })
                );

                response.Success = true;
            }
            else
            {
                response.Message = "Falha ao atualizar registro.";
            }

            return response;
        }
    }

}

