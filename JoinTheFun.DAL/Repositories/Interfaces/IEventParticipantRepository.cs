using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IEventParticipantRepository
    {
        Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId);
        Task AddAsync(EventParticipant participant);
        Task RemoveAsync(EventParticipant participant);
    }
}
