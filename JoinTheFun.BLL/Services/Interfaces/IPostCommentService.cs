using JoinTheFun.BLL.DTO.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IPostCommentService
    {
        Task<IEnumerable<PostCommentDto>> GetByPostIdAsync(int postId);
        Task AddAsync(CreatePostCommentDto dto);
        Task DeleteAsync(int commentId);
    }
}
