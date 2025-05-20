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
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDbContext _context;
        public PostLikeRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> IsLikedAsync(int postId, string userId) =>
            await _context.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);

        public async Task AddAsync(PostLike like)
        {
            _context.PostLikes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(PostLike like)
        {
            _context.PostLikes.Remove(like);
            await _context.SaveChangesAsync();
        }
    }
}
