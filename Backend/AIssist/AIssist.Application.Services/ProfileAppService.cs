using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;

namespace AIssist.Application.Services
{
    public class ProfileAppService : IProfileAppService
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileAppService(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        public Task Add(ProfileRequest entity)
        {
            return _profileService.Add(entity);
        }

        public Task Inactivate(long entityId)
        {
            return _profileService.Inactivate(entityId);
        }

        public Task<List<Profiles>> Get()
        {
            var result = _profileService.Get();
            return result;
        }

        public Task<List<Profiles>> GetById(long profileId)
        {
            var result = _profileService.GetById(profileId);
            return result;
        }

        public Task Update(ProfileRequest entity)
        {
            var profile = _mapper.Map<Profiles>(entity);

            return _profileService.Update(profile);
        }
    }
}

