using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;

namespace AIssist.Domain.Services.Interfaces
{
	public interface IProfileService
	{
        Task<List<Profiles>> GetById(long profileId);
        Task Add(ProfileRequest profile);
        Task<List<Profiles>> Get();
        Task Update(Profiles entity);
        Task Inactivate(long entityId);
    }
}

