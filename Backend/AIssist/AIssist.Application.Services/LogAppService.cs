using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Response.Log;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;

namespace AIssist.Application.Services
{
    public class LogAppService : ILogAppService
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogAppService(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<List<LogResponse>> Get()
        {
            var result = await _logService.Get();
            var mappedResult = _mapper.Map<List<LogResponse>>(result);

            return mappedResult;
        }

        public Task Add(string action, string descrip)
        {
            var log = new Logs
            {
                Action = action,
                Description = descrip,
                CreatedAt = DateTime.Now
            };
            return _logService.Add(log);
        }
    }
}

