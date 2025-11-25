using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Ticket;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Http.Response.Tickets;

namespace AIssist.Application.Services.Interfaces
{
	public interface ITicketAppService
	{
        Task<DefaultResponse> Add(TicketPostRequest entity);
        Task<List<TicketResponse>> Get();
        Task<TicketResponse?> GetByTicketNumber(string ticketNumber);
        Task<List<TicketResponse>> GetByReporterId(long reporterId);
        Task<DefaultResponse> UpdateStatus(string ticketNumber, long newStatus);
        Task<DefaultResponse> Update(TicketPutRequest entity);
        Task<DefaultResponse> UpdateAssignee(string ticketNumber, long assigneeId);
    }
}

