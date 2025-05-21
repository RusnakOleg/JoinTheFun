using BlazorFrontend.DTO.Comments;

namespace BlazorFrontend.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<PostCommentDto>> GetByPostIdAsync(int postId);
        Task<bool> AddAsync(CreatePostCommentDto dto);
        Task<bool> DeleteAsync(int commentId);
    }
}
