using AIssist.Domain.Entities;
using AIssist.Domain.Services.Interfaces;
using Supabase;

namespace AIssist.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly Client _supabaseclient;

        public UserService(Client supabaseclient)
        {
            _supabaseclient = supabaseclient;
        }

        public async Task Add(Users entity)
        {
            await _supabaseclient.From<Users>().Insert(entity);
        }

        public async Task Inactivate(long entityId)
        {
            await _supabaseclient
                .From<Users>()
                .Where(w => w.Id == entityId)
                .Set(s => s.Active, false)
                .Update();
        }

        public async Task<List<Users>> Get()
        {
            var result = await _supabaseclient
                .From<Users>()
                .Get();

            return result.Models;
        }

        public async Task<List<Users>> GetById(long entityId)
        {
            var result = await _supabaseclient
                .From<Users>()
                .Where(w => w.Id == entityId)
                .Get();

            return result.Models;
        }

        public async Task Update(Users entity)
        {
            await _supabaseclient
                .From<Users>()
                .Where(w => w.Id == entity.Id)
                .Set(s => s.Name, entity.Name)
                .Set(s => s.Email, entity.Email)
                .Set(s => s.Updated_At, entity.Updated_At)
                .Update();
        }
    }
}

