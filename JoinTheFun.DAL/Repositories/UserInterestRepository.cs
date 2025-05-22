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
    public class UserInterestRepository : IUserInterestRepository
    {
        private readonly ApplicationDbContext _context;
        public UserInterestRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<UserInterest>> GetInterestsByProfileIdAsync(int profileId) =>
    await _context.UserInterests
        .Where(ui => ui.ProfileId == profileId)
        .ToListAsync();


        public async Task AddAsync(UserInterest interest)
        {
            _context.UserInterests.Add(interest);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserInterest interest)
        {
            _context.UserInterests.Remove(interest);
            await _context.SaveChangesAsync();
        }
    }
}
