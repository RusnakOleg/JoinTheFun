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
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly ApplicationDbContext _context;
        public PostCommentRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<PostComment>> GetByPostIdAsync(int postId) =>
            await _context.PostComments.Where(c => c.PostId == postId).ToListAsync();

        public async Task AddAsync(PostComment comment)
        {
            _context.PostComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _context.PostComments.FindAsync(commentId);
            if (comment != null)
            {
                _context.PostComments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
