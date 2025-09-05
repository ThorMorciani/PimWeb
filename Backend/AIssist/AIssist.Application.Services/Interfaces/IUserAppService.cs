using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;

namespace AIssist.Application.Services.Interfaces
{
	public interface IUserAppService
	{
        Task Add(UserPostRequest userRequest);
        Task<List<Users>> GetById(long entityId);
        Task Update(UserPutRequest userRequest);
        Task<List<Users>> Get();
        Task Inactivate(long entityId);
    }
}

