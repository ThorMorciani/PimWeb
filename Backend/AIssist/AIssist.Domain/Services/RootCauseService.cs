using AIssist.Domain.Entities;
using AIssist.Domain.Services.Interfaces;
using Supabase;

namespace AIssist.Domain.Services
{
    public class RootCauseService : IRootCauseService
    {
        private readonly Client _supabaseclient;

        public RootCauseService(Client supabaseclient)
        {
            _supabaseclient = supabaseclient;
        }

        public async Task Add(RootCauses entity)
        {
            await _supabaseclient.From<RootCauses>().Insert(entity);
        }

        public async Task Inactivate(long entityId)
        {
            await _supabaseclient
                .From<RootCauses>()
                .Where(w => w.Id == entityId)
                .Set(s => s.Active, false)
                .Update();
        }

        public async Task<List<RootCauses>> Get()
        {
            var result = await _supabaseclient
                .From<RootCauses>()
                .Get();

            return result.Models;
        }

        public async Task<List<RootCauses>> GetById(long entityId)
        {
            var result = await _supabaseclient
                .From<RootCauses>()
                .Where(w => w.Id == entityId)
                .Get();

            return result.Models;
        }

        public async Task Update(RootCauses entity)
        {
            await _supabaseclient
                .From<RootCauses>()
                .Where(w => w.Id == entity.Id)
                .Set(s => s.Root_Cause, entity.Root_Cause)
                .Set(s => s.Criticality_Id, entity.Criticality_Id)
                .Set(s => s.Updated_At, entity.Updated_At)
                .Update();
        }
    }
}

