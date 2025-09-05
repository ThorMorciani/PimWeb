using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AIssist.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAppService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public Task Add(UserPostRequest entity)
        {
            var user = _mapper.Map<Users>(entity);

            var hashedPassword = new PasswordHasher<Users>()
                .HashPassword(user, entity.Password);
            user.Password = hashedPassword;

            return _userService.Add(user);
        }

        public Task Inactivate(long entityId)
        {
            return _userService.Inactivate(entityId);
        }

        public Task<List<Users>> Get()
        {
            var result = _userService.Get();
            return result;
        }

        public Task<List<Users>> GetById(long profileId)
        {
            var result = _userService.GetById(profileId);
            return result;
        }

        public Task Update(UserPutRequest entity)
        {
            var user = _mapper.Map<Users>(entity);

            return _userService.Update(user);
        }
    }
}

