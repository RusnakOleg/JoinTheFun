using JoinTheFun.DAL.Context;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Event>> GetAllAsync() =>
            await _context.Events.Include(e => e.Creator).ToListAsync();

        public async Task<Event?> GetByIdAsync(int id) =>
            await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.EventId == id);

        public async Task AddAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Event>> GetByLocationAsync(string location)
        {
            return await _context.Events
                .Where(e => e.Location.ToLower().Contains(location.ToLower()))
                .Include(e => e.Creator)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByUserInterestsAsync(IEnumerable<int> interestIds)
        {
            var userIds = await _context.Profiles
                .Where(p => p.Interests.Any(i => interestIds.Contains(i.InterestId)))
                .Select(p => p.UserId)
                .ToListAsync();

            return await _context.Events
                .Where(e => userIds.Contains(e.CreatorId))
                .Include(e => e.Creator)
                .ToListAsync();
        }

    }
}
