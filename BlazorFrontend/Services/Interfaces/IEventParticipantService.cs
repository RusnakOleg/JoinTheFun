using BlazorFrontend.DTO.Event_Participants;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IEventParticipantService
    {
        Task<List<EventParticipantDto>> GetByEventIdAsync(int eventId);
        Task<bool> AddAsync(EventParticipantDto dto);
        Task<bool> RemoveAsync(RemoveEventParticipantDto dto);
    }
}
