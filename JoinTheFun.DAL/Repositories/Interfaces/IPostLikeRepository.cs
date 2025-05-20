using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IPostLikeRepository
    {
        Task<bool> IsLikedAsync(int postId, string userId);
        Task AddAsync(PostLike like);
        Task RemoveAsync(PostLike like);
    }
}
