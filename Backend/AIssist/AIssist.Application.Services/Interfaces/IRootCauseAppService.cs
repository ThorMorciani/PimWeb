using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Http.Response.RootCause;

namespace AIssist.Application.Services.Interfaces
{
	public interface IRootCauseAppService
	{
        Task<DefaultResponse> Add(RootCausePostRequest rootCauseRequest);
        Task<RootCauseResponse?> GetById(long entityId);
        Task<DefaultResponse> Update(RootCausePutRequest rootCauseRequest);
        Task<List<RootCauseResponse>> Get();
        Task<DefaultResponse> Inactivate(long entityId);
    }
}

