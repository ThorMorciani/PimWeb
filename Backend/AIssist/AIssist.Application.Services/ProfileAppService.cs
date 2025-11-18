using System.Text.Json;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;

namespace AIssist.Application.Services
{
    public class ProfileAppService : IProfileAppService
    {
        private readonly IProfileService _profileService;
        private readonly ILogAppService _logAppService;
        private readonly IMapper _mapper;

        public ProfileAppService(IProfileService profileService, IMapper mapper, ILogAppService logAppService)
        {
            _profileService = profileService;
            _logAppService = logAppService;
            _mapper = mapper;
        }

        public async Task<DefaultResponse> Add(ProfileRequest entity)
        {
            var response = new DefaultResponse();
            var profile = _mapper.Map<Profiles>(entity);

            try
            {
                await _profileService.Add(profile);
            }
            catch
            {
                response.Message = "Falha ao salvar registro.";
                return response;
            }

            try
            {
                await _logAppService.Add("Inserção", JsonSerializer.Serialize(entity));
                response.Success = true;
            }
            catch
            {
                response.Message = "Falha ao salvar o log da operação.";
            }

            return response;
        }

        public async Task<DefaultResponse> Inactivate(long entityId)
        {
            var response = new DefaultResponse();

            try
            {
                await _profileService.Inactivate(entityId);
            }
            catch
            {
                response.Message = "Falha ao inativar registro.";
                return response;
            }

            try
            {
                var logData = JsonSerializer.Serialize(new { id = entityId, entity = "Profile" });
                await _logAppService.Add("Inativação", logData);
                response.Success = true;
            }
            catch
            {
                response.Message = "Falha ao salvar o log da operação.";
            }

            return response;
        }

        public Task<List<Profiles>> Get()
            => _profileService.Get();

        public Task<Profiles?> GetById(long profileId)
            => _profileService.GetById(profileId);

        public async Task<DefaultResponse> Update(ProfilePutRequest entity)
        {
            var response = new DefaultResponse();
            var profile = _mapper.Map<Profiles>(entity);

            try
            {
                await _profileService.Update(profile);
            }
            catch
            {
                response.Message = "Falha ao atualizar registro.";
                return response;
            }

            try
            {
                await _logAppService.Add("Atualização", JsonSerializer.Serialize(entity));
                response.Success = true;
            }
            catch
            {
                response.Message = "Falha ao salvar o log da operação.";
            }

            return response;
        }
    }

}

