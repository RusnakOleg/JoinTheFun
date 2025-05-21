using System.Net.Http.Json;
using BlazorFrontend.DTO.Interests;
using BlazorFrontend.DTO.UserInterest;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class UserInterestService : IUserInterestService
    {
        private readonly HttpClient _http;

        public UserInterestService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<InterestDto>> GetByProfileIdAsync(int profileId)
        {
            return await _http.GetFromJsonAsync<List<InterestDto>>($"userinterest/{profileId}");
        }

        public async Task<bool> AddAsync(AddUserInterestDto dto)
        {
            var response = await _http.PostAsJsonAsync("userinterest", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(AddUserInterestDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "userinterest")
            {
                Content = JsonContent.Create(dto)
            };

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
