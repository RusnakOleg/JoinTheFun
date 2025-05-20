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
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> IsFollowingAsync(string followerId, string followedId) =>
            await _context.Follows.AnyAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);

        public async Task AddAsync(Follow follow)
        {
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Follow follow)
        {
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
        }
    }
}
