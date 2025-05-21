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
    public class InterestRepository : IInterestRepository
    {
        private readonly ApplicationDbContext _context;
        public InterestRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Interest>> GetAllAsync() =>
            await _context.Interests.ToListAsync();

        public async Task<Interest?> GetByIdAsync(int id) =>
            await _context.Interests.FindAsync(id);

        public async Task AddAsync(Interest interest)
        {
            _context.Interests.Add(interest);
            await _context.SaveChangesAsync();
        }

    }
}
