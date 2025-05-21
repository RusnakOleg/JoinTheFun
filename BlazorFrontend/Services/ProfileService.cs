using BlazorFrontend.DTO.Profiles;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _http;

        public ProfileService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProfileDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ProfileDto>>("profile");
        }

        public async Task<ProfileDto?> GetByUserIdAsync(string userId)
        {
            return await _http.GetFromJsonAsync<ProfileDto>($"profile/{userId}");
        }



        public async Task<List<ProfileDto>> GetByCityAsync(string city)
        {
            return await _http.GetFromJsonAsync<List<ProfileDto>>($"profile/by-city?city={city}");
        }

        public async Task<List<ProfileDto>> GetByInterestIdAsync(int interestId)
        {
            return await _http.GetFromJsonAsync<List<ProfileDto>>($"profile/by-interest?interestId={interestId}");
        }

        public async Task UpdateAsync(UpdateProfileDto dto, string userId)
        {
            await _http.PutAsJsonAsync($"profile/{userId}", dto);
        }

    }

}
