using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;

namespace AIssist.Application.Services.Interfaces
{
	public interface IUserAppService
	{
        Task Add(UserRequest userRequest);
        Task<List<Users>> GetById(long entityId);
        Task Update(UserRequest userRequest);
        Task<List<Users>> Get();
        Task Inactivate(long entityId);
    }
}

