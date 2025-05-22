using BlazorFrontend.DTO.Events;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<List<EventDto>> GetByLocationAsync(string city);
        Task<List<EventDto>> GetByUserInterestsAsync(string userId);
        Task<bool> CreateAsync(CreateEventDto dto);
        Task DeleteAsync(int id);

    }
}
