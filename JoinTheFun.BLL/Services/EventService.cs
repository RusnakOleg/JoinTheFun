using AutoMapper;
using JoinTheFun.BLL.DTO.Events;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly IProfileRepository _profileRepo;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepo, IProfileRepository profileRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _profileRepo = profileRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var events = await _eventRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<IEnumerable<EventDto>> GetByLocationAsync(string location)
        {
            var events = await _eventRepo.GetByLocationAsync(location);
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<IEnumerable<EventDto>> GetByUserInterestsAsync(string userId)
        {
            var profile = await _profileRepo.GetByUserIdAsync(userId);
            var interestIds = profile?.Interests.Select(i => i.InterestId) ?? Enumerable.Empty<int>();
            var events = await _eventRepo.GetByUserInterestsAsync(interestIds);
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task CreateAsync(CreateEventDto dto)
        {
            var entity = _mapper.Map<Event>(dto);
            await _eventRepo.AddAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _eventRepo.DeleteAsync(id);
        }

    }
}
