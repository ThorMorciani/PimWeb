using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;
using AIssist.Domain.Services.Interfaces;
using System.Text.Json;
using AutoMapper;
using AIssist.Domain.Http.Response;

namespace AIssist.Application.Services
{
    public class RootCauseAppService : IRootCauseAppService
    {
        private readonly IRootCauseService _rootCauseService;
        private readonly ILogAppService _logAppService;
        private readonly IMapper _mapper;

        public RootCauseAppService(
            IRootCauseService rootCauseService,
            IMapper mapper,
            ILogAppService logAppService)
        {
            _rootCauseService = rootCauseService;
            _logAppService = logAppService;
            _mapper = mapper;
        }

        public async Task<DefaultResponse> Add(RootCausePostRequest entity)
        {
            var response = new DefaultResponse();
            var rootCause = _mapper.Map<RootCause>(entity);

            var rootCauseResult = await _rootCauseService.Add(rootCause);

            if (rootCauseResult)
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

        public async Task<DefaultResponse> Inactivate(long entityId)
        {
            var response = new DefaultResponse();
            var rootCauseResult = await _rootCauseService.Inactivate(entityId);

            if (rootCauseResult)
            {
                await _logAppService.Add(
                    "Inativação",
                    JsonSerializer.Serialize(new { id = entityId, entity = "RootCause" })
                );

                response.Success = true;
            }
            else
            {
                response.Message = "Falha ao inativar registro.";
            }

            return response;
        }

        public Task<List<RootCause>> Get()
        {
            return _rootCauseService.Get();
        }

        public Task<RootCause?> GetById(long rootCauseId)
        {
            return _rootCauseService.GetById(rootCauseId);
        }

        public async Task<DefaultResponse> Update(RootCausePutRequest entity)
        {
            var response = new DefaultResponse();
            var rootCause = _mapper.Map<RootCause>(entity);

            var rootCauseResult = await _rootCauseService.Update(rootCause);

            if (rootCauseResult)
            {
                await _logAppService.Add(
                    "Atualização",
                    JsonSerializer.Serialize(entity)
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

