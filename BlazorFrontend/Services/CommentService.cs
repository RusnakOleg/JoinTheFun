using System.Net.Http.Json;
using BlazorFrontend.DTO.Comments;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _http;

        public CommentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PostCommentDto>> GetByPostIdAsync(int postId)
        {
            return await _http.GetFromJsonAsync<List<PostCommentDto>>($"comments/by-post/{postId}");
        }

        public async Task<bool> AddAsync(CreatePostCommentDto dto)
        {
            var response = await _http.PostAsJsonAsync("comments", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            var response = await _http.DeleteAsync($"comments/{commentId}");
            return response.IsSuccessStatusCode;
        }
    }
}
