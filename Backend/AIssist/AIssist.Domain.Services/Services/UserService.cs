using AIssist.Domain.Entities;
using AIssist.Domain.Interfaces;
using AIssist.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIssist.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _context;

        public UserService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Users entity)
        {
            try
            {
                await _context.Users.AddAsync(entity);
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
                var entity = await _context.Users.FindAsync(entityId);
                if (entity == null)
                    return false;

                entity.Active = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Users>> Get()
        {
            return await _context.Users
                    .Include(p => p.Profile)
                    .ToListAsync();
        }

        public async Task<Users?> GetById(long userId)
        {
            return await _context.Users
            .FirstOrDefaultAsync(rc => rc.Id == userId);
        }

        public async Task<Users?> GetByUsername(string username)
        {
            return await _context.Users
            .FirstOrDefaultAsync(rc => rc.Username == username);
        }

        public async Task<bool> Update(Users user)
        {
            try
            {
                var entity = await _context.Users.FindAsync(user.Id);
                if (entity == null)
                    return false;

                _context.Atualizar(entity, user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateRefreshToken(Users user)
        {
            try
            {
                var entity = await _context.Users.FindAsync(user.Id);
                if (entity == null)
                    return false;

                _context.Atualizar(entity, user);
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

