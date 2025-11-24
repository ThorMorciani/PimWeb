using AIssist.Domain.Entities;
using AIssist.Domain.Http.Response.Users;

namespace AIssist.Domain.Services.Interfaces
{
	public interface IUserService
	{
        Task<bool> Add(Users user);
        Task<Users?> GetById(long entityId);
        Task<bool> Update(Users user);
        Task<List<Users>> Get();
        Task<bool> Inactivate(long entityId);
        Task<Users?> GetByUsername(string username);
        Task<bool> UpdateRefreshToken(Users entity);
        Task<List<Users>> GetTechnicians();
    }
}

