using JoinTheFun.BLL.DTO.Likes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IPostLikeService
    {
        Task<bool> IsLikedAsync(int postId, string userId);
        Task LikeAsync(PostLikeDto dto);
        Task UnlikeAsync(PostLikeDto dto);
    }
}
