using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;

namespace AIssist.Application.Services.Interfaces
{
	public interface IRootCauseAppService
	{
        Task Add(RootCausePostRequest rootCauseRequest);
        Task<List<RootCauses>> GetById(long entityId);
        Task Update(RootCausePutRequest rootCauseRequest);
        Task<List<RootCauses>> Get();
        Task Inactivate(long entityId);
    }
}

