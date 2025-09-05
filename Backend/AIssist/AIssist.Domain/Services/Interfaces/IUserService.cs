using AIssist.Domain.Entities;

namespace AIssist.Domain.Services.Interfaces
{
	public interface IUserService
	{
        Task Add(Users user);
        Task<List<Users>> GetById(long entityId);
        Task Update(Users user);
        Task<List<Users>> Get();
        Task Inactivate(long entityId);
        Task<Users> GetByUsername(string username);
    }
}

