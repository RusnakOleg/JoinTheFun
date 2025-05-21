using BlazorFrontend.DTO.Likes;

namespace BlazorFrontend.Services.Interfaces
{
    public interface ILikeService
    {
        Task<bool> IsLikedAsync(int postId, string userId);
        Task<bool> LikeAsync(PostLikeDto dto);
        Task<bool> UnlikeAsync(PostLikeDto dto);
    }
}
