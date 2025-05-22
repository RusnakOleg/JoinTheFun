using System.Net.Http.Json;
using BlazorFrontend.DTO.Events;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _http;

        public EventService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<EventDto>>("events");
        }

        public async Task<List<EventDto>> GetByLocationAsync(string city)
        {
            return await _http.GetFromJsonAsync<List<EventDto>>($"events/by-location?city={city}");
        }

        public async Task<List<EventDto>> GetByUserInterestsAsync(string userId)
        {
            return await _http.GetFromJsonAsync<List<EventDto>>($"events/by-user-interests?userId={userId}");
        }

        public async Task<bool> CreateAsync(CreateEventDto dto)
        {
            var response = await _http.PostAsJsonAsync("events", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task DeleteAsync(int id)
        {
            await _http.DeleteAsync($"events/{id}");
        }

    }
}
