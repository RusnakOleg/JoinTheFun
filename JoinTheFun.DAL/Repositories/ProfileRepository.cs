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
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context) => _context = context;

        public async Task<Profile?> GetByUserIdAsync(string userId)
        {
            return await _context.Profiles
                .Include(p => p.ApplicationUser)
                .Include(p => p.Interests)
                    .ThenInclude(ui => ui.Interest) // ⬅ ОБОВ’ЯЗКОВО
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }


        public async Task AddAsync(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetByCityAsync(string city)
        {
            return await _context.Profiles
                .Include(p => p.Interests)
            .ThenInclude(ui => ui.Interest)
        .Where(p => p.City.ToLower().Contains(city.ToLower()))
        .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByInterestIdAsync(int interestId)
        {
            return await _context.Profiles
                .Include(p => p.ApplicationUser)
                .Include(p => p.Interests)
            .ThenInclude(i => i.Interest)
        .Where(p => p.Interests.Any(i => i.InterestId == interestId))
        .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profiles
                .Include(p => p.ApplicationUser)
                .Include(p => p.Interests)
                    .ThenInclude(i => i.Interest)
                .ToListAsync();
        }


    }
}
