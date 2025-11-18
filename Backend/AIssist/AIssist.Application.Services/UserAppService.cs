using System.Text.Json;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Http.Response.Users;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AIssist.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly ILogAppService _logAppService;
        private readonly IMapper _mapper;

        public UserAppService(
            IUserService userService,
            IMapper mapper,
            ILogAppService logAppService)
        {
            _userService = userService;
            _logAppService = logAppService;
            _mapper = mapper;
        }

        public async Task<DefaultResponse> Add(UserPostRequest entity)
        {
            var response = new DefaultResponse();

            var user = _mapper.Map<Users>(entity);
            user.Password = new PasswordHasher<Users>()
                .HashPassword(user, entity.Password);

            try
            {
                await _userService.Add(user);
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
                await _userService.Inactivate(entityId);
            }
            catch
            {
                response.Message = "Falha ao salvar registro.";
                return response;
            }

            try
            {
                var logData = JsonSerializer.Serialize(new { id = entityId, entity = "User" });
                await _logAppService.Add("Inativação", logData);
                response.Success = true;
            }
            catch
            {
                response.Message = "Falha ao salvar o log da operação.";
            }

            return response;
        }

        public async Task<List<UserResponse>> Get()
        {
            var users = await _userService.Get();
            var result = _mapper.Map<List<UserResponse>>(users);
            return result;
        }

        public Task<Users?> GetById(long userId)
            => _userService.GetById(userId);

        public async Task<DefaultResponse> Update(UserPutRequest entity)
        {
            var response = new DefaultResponse();

            var user = await GetById(entity.Id);

            if (user == null)
            {
                response.Message = "Usuário não encontrado.";
                return response;
            }

            _mapper.Map(entity, user);

            try
            {
                await _userService.Update(user);
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

