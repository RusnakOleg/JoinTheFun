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
    public class EventParticipantRepository : IEventParticipantRepository
    {
        private readonly ApplicationDbContext _context;
        public EventParticipantRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId) =>
            await _context.EventParticipants.Where(p => p.EventId == eventId).ToListAsync();

        public async Task AddAsync(EventParticipant participant)
        {
            _context.EventParticipants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(EventParticipant participant)
        {
            _context.EventParticipants.Remove(participant);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<EventParticipant>> GetByUserIdAsync(string userId)
        {
            return await _context.EventParticipants
                .Include(p => p.Event) 
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }


    }
}
