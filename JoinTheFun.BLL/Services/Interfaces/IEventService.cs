using JoinTheFun.BLL.DTO.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<IEnumerable<EventDto>> GetByLocationAsync(string location);
        Task<IEnumerable<EventDto>> GetByUserInterestsAsync(string userId);
        Task CreateAsync(CreateEventDto dto);
        Task DeleteAsync(int id);

    }
}
