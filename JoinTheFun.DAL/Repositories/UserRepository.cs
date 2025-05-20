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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task<ApplicationUser?> GetByIdAsync(string userId) =>
            await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
            await _context.Users.ToListAsync();
    }
}
