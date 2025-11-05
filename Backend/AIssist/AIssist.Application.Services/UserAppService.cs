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

        public UserAppService(IUserService userService, IMapper mapper, ILogAppService logAppService)
        {
            _userService = userService;
            _logAppService = logAppService;
            _mapper = mapper;
        }

        public Task<DefaultResponse> Add(UserPostRequest entity)
        {
            var response = new DefaultResponse();
            var user = _mapper.Map<Users>(entity);

            var hashedPassword = new PasswordHasher<Users>()
                .HashPassword(user, entity.Password);
            user.Password = hashedPassword;

            var userResult = _userService.Add(user);
            userResult.Wait();

            if (userResult.IsCompletedSuccessfully)
            {
                var logResult = _logAppService.Add("Inserção", JsonSerializer.Serialize(entity));
                logResult.Wait();

                if (logResult.IsCompletedSuccessfully)
                    response.Success = true;
                else
                    response.Message = "Falha ao salvar o log da operação.";
            }
            else
                response.Message = "Falha ao salvar registro.";

            return Task.FromResult(response);
        }

        public Task<DefaultResponse> Inactivate(long entityId)
        {
            var response = new DefaultResponse();
            var userResult = _userService.Inactivate(entityId);
            userResult.Wait();

            if (userResult.IsCompletedSuccessfully)
            {
                var logResult = _logAppService.Add("Inativação", JsonSerializer.Serialize(new { id = entityId, entity = "User" }));
                logResult.Wait();

                if (logResult.IsCompletedSuccessfully)
                    response.Success = true;
                else
                    response.Message = "Falha ao salvar o log da operação.";
            }
            else
                response.Message = "Falha ao salvar registro.";

            return Task.FromResult(response);
        }

        public Task<List<UserResponse>> Get()
        {
            var users = _userService.Get();
            users.Wait();

            var result = _mapper.Map<List<UserResponse>>(users.Result);
            return Task.FromResult(result);
        }

        public Task<Users?> GetById(long userId)
        {
            var result = _userService.GetById(userId);
            return result;
        }

        public async Task<DefaultResponse> Update(UserPutRequest entity)
        {
            var userToMap = await GetById(entity.Id);
            var response = new DefaultResponse();
            _mapper.Map(entity, userToMap);

            var userResult = _userService.Update(userToMap);
            userResult.Wait();

            if (userResult.IsCompletedSuccessfully)
            {
                var logResult = _logAppService.Add("Atualização", JsonSerializer.Serialize(entity));
                logResult.Wait();

                if (logResult.IsCompletedSuccessfully)
                    response.Success = true;
                else
                    response.Message = "Falha ao salvar o log da operação.";
            }
            else
                response.Message = "Falha ao atualizar registro.";

            return response;
        }
    }
}

