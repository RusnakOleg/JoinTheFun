using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        Task<IEnumerable<PostComment>> GetByPostIdAsync(int postId);
        Task AddAsync(PostComment comment);
        Task DeleteAsync(int commentId);
    }
}
