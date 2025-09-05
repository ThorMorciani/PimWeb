using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response;

namespace AIssist.Application.Api.Services.Interfaces
{
	public interface IAuthService
	{
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}

