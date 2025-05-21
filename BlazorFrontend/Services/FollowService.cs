using System.Net.Http.Json;
using BlazorFrontend.DTO.Folowers;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class FollowService : IFollowService
    {
        private readonly HttpClient _http;

        public FollowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> IsFollowingAsync(string followerId, string followedId)
        {
            var result = await _http.GetFromJsonAsync<bool>($"follow/is-following?followerId={followerId}&followedId={followedId}");
            return result;
        }

        public async Task<bool> FollowAsync(FollowDto dto)
        {
            var response = await _http.PostAsJsonAsync("follow", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnfollowAsync(FollowDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "follow")
            {
                Content = JsonContent.Create(dto)
            };

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
