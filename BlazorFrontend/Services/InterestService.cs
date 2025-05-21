using System.Net.Http.Json;
using BlazorFrontend.DTO.Interests;
using BlazorFrontend.Services.Interfaces;


namespace BlazorFrontend.Services
{
    public class InterestService : IInterestService
    {
        private readonly HttpClient _http;

        public InterestService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<InterestDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<InterestDto>>("interests");
        }

        public async Task<bool> CreateAsync(CreateInterestDto dto)
        {
            var response = await _http.PostAsJsonAsync("interests", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
