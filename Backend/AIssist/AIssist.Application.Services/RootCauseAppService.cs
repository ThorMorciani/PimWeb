using System;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;

namespace AIssist.Application.Services
{
    public class RootCauseAppService : IRootCauseAppService
    {
        private readonly IRootCauseService _rootCauseService;
        private readonly IMapper _mapper;

        public RootCauseAppService(IRootCauseService rootCauseService, IMapper mapper)
        {
            _rootCauseService = rootCauseService;
            _mapper = mapper;
        }

        public Task Add(RootCauseRequest entity)
        {
            var user = _mapper.Map<RootCauses>(entity);

            return _rootCauseService.Add(user);
        }

        public Task Inactivate(long entityId)
        {
            return _rootCauseService.Inactivate(entityId);
        }

        public Task<List<RootCauses>> Get()
        {
            var result = _rootCauseService.Get();
            return result;
        }

        public Task<List<RootCauses>> GetById(long profileId)
        {
            var result = _rootCauseService.GetById(profileId);
            return result;
        }

        public Task Update(RootCauseRequest entity)
        {
            var user = _mapper.Map<RootCauses>(entity);

            return _rootCauseService.Update(user);
        }
    }
}

