using AIssist.Domain.Entities;
using AIssist.Domain.Interfaces;
using AIssist.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIssist.Domain.Services
{
    public class RootCauseService : IRootCauseService
    {
        private readonly IAppDbContext _context;

        public RootCauseService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(RootCause entity)
        {
            try
            {
                await _context.RootCauses.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Inactivate(long entityId)
        {
            try
            {
                var entity = await _context.RootCauses.FindAsync(entityId);
                if (entity == null)
                    return false;

                entity.Active = !entity.Active;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RootCause>> Get()
        {
            return await _context.RootCauses.ToListAsync();
        }

        public async Task<RootCause?> GetById(long rootCauseId)
        {
            return await _context.RootCauses
            .FirstOrDefaultAsync(rc => rc.Id == rootCauseId);
        }

        public async Task<bool> Update(RootCause rootCause)
        {
            try
            {
                var entity = await _context.RootCauses.FindAsync(rootCause.Id);
                if (entity == null)
                    return false;

                _context.Atualizar(entity,rootCause);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

