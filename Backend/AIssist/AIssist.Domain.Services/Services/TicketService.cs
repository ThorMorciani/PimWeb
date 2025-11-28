using AIssist.Domain.Entities;
using AIssist.Domain.Enums;
using AIssist.Domain.Interfaces;
using AIssist.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIssist.Domain.Services
{
	public class TicketService : ITicketService
	{
        private readonly IAppDbContext _context;

        public TicketService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Tickets entity)
        {
            try
            {
                await _context.Tickets.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Tickets>> Get()
        {
            return await _context
                    .Tickets
                    .Include(t => t.RootCause)
                    .Include(t => t.Assignee)
                    .Include(t => t.Reporter)
                    .ToListAsync();
        }

        public async Task<Tickets?> GetByTicketNumber(string ticketNumber)
        {
            return await _context.Tickets
                    .Include(t => t.RootCause)
                    .Include(t => t.Assignee)
                    .Include(t => t.Reporter)
                    .FirstOrDefaultAsync(rc => rc.TicketNumber == ticketNumber);
        }

        public async Task<List<Tickets>> GetByReporterId(long reporterId)
        {
            return await _context.Tickets
                    .Include(t => t.RootCause)
                    .Include(t => t.Assignee)
                    .Include(t => t.Reporter)
                    .Where(t => t.ReporterId == reporterId)
                    .ToListAsync();
        }

        public async Task<bool> UpdateStatus(string ticketNumber, long newStatus)
        {
            try
            {
                var entity = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
                if (entity == null)
                    return false;

                if (Enum.IsDefined(typeof(TicketStatus), (int)newStatus))
                {
                    entity.Status = (TicketStatus)(int)newStatus;
                }

                entity.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAssignee(string ticketNumber, long assigneeId)
        {
            try
            {
                var entity = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

                if (entity == null)
                    return false;

                entity.AssigneeId = assigneeId;
                entity.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Tickets ticket)
        {
            try
            {
                var entity = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.TicketNumber == ticket.TicketNumber);
                if (entity == null)
                    return false;

                entity.Description = ticket.Description;
                entity.Solution = ticket.Solution;
                entity.UpdatedAt = DateTime.Now;
                
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

