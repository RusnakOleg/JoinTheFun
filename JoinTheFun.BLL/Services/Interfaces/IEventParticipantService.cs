using JoinTheFun.BLL.DTO.Event_Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IEventParticipantService
    {
        Task<IEnumerable<EventParticipantDto>> GetByEventIdAsync(int eventId);
        Task AddAsync(EventParticipantDto dto);
        Task RemoveAsync(EventParticipantDto dto);
    }
}
