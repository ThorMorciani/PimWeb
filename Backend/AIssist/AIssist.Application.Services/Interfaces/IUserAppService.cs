using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response;
using AIssist.Domain.Http.Response.Users;

namespace AIssist.Application.Services.Interfaces
{
	public interface IUserAppService
	{
        Task<DefaultResponse> Add(UserPostRequest userRequest);
        Task<Users?> GetById(long entityId);
        Task<DefaultResponse> Update(UserPutRequest userRequest);
        Task<List<UserResponse>> Get();
        Task<DefaultResponse> Inactivate(long entityId);
        Task<List<UserResponse>> GetTechnicians();
    }
}

