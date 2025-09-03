using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;
using AIssist.Domain.Services.Interfaces;
using Supabase;

namespace AIssist.Domain.Services
{
    public class ProfileService : IProfileService
    {
        private readonly Client _supabaseclient;

        public ProfileService(Client supabaseclient)
        {
            _supabaseclient = supabaseclient;
        }

        public async Task Add(ProfileRequest entity)
        {
            var profile = new Profiles
            {
                Profile = "coordenador",
                Created_By = "thor",
                Updated_At = DateTime.Now,
                Updated_By = "thor"
            };
            await _supabaseclient.From<Profiles>().Insert(profile);
        }

        public async Task Inactivate(long entityId)
        {
            await _supabaseclient
                .From<Profiles>()
                .Where(w => w.Id == entityId)
                .Set(s => s.Active, false)
                .Update();
        }

        public async Task<List<Profiles>> Get()
        {
            var result = await _supabaseclient
                .From<Profiles>()
                .Get();

            return result.Models;
        }

        public async Task<List<Profiles>> GetById(long profileId)
        {
            var result = await _supabaseclient
                .From<Profiles>()
                .Where(w => w.Id == profileId)
                .Get();

            return result.Models;
        }

        public async Task Update(Profiles entity)
        {
            await _supabaseclient
                .From<Profiles>()
                .Where(w => w.Id == entity.Id)
                .Set(s => s.Profile, entity.Profile)
                .Set(s => s.Updated_At, entity.Updated_At)
                .Update();
        }
    }
}

