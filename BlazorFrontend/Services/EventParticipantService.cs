using System.Net.Http.Json;
using BlazorFrontend.DTO.Event_Participants;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly HttpClient _http;

        public EventParticipantService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EventParticipantDto>> GetByEventIdAsync(int eventId)
        {
            return await _http.GetFromJsonAsync<List<EventParticipantDto>>($"eventparticipants/{eventId}");
        }

        public async Task<bool> AddAsync(EventParticipantDto dto)
        {
            var response = await _http.PostAsJsonAsync("eventparticipants", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(RemoveEventParticipantDto dto)
        {
            var response = await _http.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("eventparticipants", UriKind.Relative),
                Content = JsonContent.Create(dto)
            });

            return response.IsSuccessStatusCode;
        }
    }
}
