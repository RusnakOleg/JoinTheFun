using System.Net.Http.Json;
using BlazorFrontend.DTO.Likes;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class LikeService : ILikeService
    {
        private readonly HttpClient _http;

        public LikeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> IsLikedAsync(int postId, string userId)
        {
            return await _http.GetFromJsonAsync<bool>($"likes/is-liked?postId={postId}&userId={userId}");
        }

        public async Task<bool> LikeAsync(PostLikeDto dto)
        {
            var response = await _http.PostAsJsonAsync("likes", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnlikeAsync(PostLikeDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "likes")
            {
                Content = JsonContent.Create(dto)
            };

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
