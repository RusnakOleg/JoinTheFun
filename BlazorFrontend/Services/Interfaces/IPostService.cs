using BlazorFrontend.DTO.Posts;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllAsync();
        Task<PostDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreatePostDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<PostDto>> GetPostsByFollowingsAsync(string userId);
    }
}
