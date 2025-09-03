using AIssist.Domain.Entities;

namespace AIssist.Domain.Services.Interfaces
{
	public interface IRootCauseService
	{
        Task Add(RootCauses rootCause);
        Task<List<RootCauses>> GetById(long entityId);
        Task Update(RootCauses rootCause);
        Task<List<RootCauses>> Get();
        Task Inactivate(long entityId);
    }
}

