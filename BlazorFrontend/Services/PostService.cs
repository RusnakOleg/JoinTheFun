using System.Net.Http.Json;
using BlazorFrontend.DTO.Posts;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient _http;

        public PostService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PostDto>>("posts");
        }

        public async Task<PostDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PostDto>($"posts/{id}");
        }

        public async Task<bool> CreateAsync(CreatePostDto dto)
        {
            var response = await _http.PostAsJsonAsync("posts", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"posts/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PostDto>> GetPostsByFollowingsAsync(string userId)
        {
            return await _http.GetFromJsonAsync<List<PostDto>>($"posts/following/{userId}");
        }
    }
}
