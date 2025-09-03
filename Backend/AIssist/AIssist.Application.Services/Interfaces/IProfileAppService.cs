using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;

namespace AIssist.Application.Services.Interfaces
{
	public interface IProfileAppService
	{
        Task Add(ProfileRequest profileRequest);
        Task<List<Profiles>> GetById(long profileId);
        Task Update(ProfileRequest profileRequest);
        Task<List<Profiles>> Get();
        Task Inactivate(long entityId);
    }
}

