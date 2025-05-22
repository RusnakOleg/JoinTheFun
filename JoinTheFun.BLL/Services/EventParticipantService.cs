using AutoMapper;
using JoinTheFun.BLL.DTO.Event_Participants;
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
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IEventParticipantRepository _repo;
        private readonly IMapper _mapper;

        public EventParticipantService(IEventParticipantRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventParticipantDto>> GetByEventIdAsync(int eventId)
        {
            var list = await _repo.GetByEventIdAsync(eventId);
            return _mapper.Map<IEnumerable<EventParticipantDto>>(list);
        }

        public async Task AddAsync(EventParticipantDto dto)
        {
            var entity = _mapper.Map<EventParticipant>(dto);
            await _repo.AddAsync(entity);
        }

        public async Task RemoveAsync(RemoveEventParticipantDto dto)
        {
            var entity = _mapper.Map<EventParticipant>(dto);
            await _repo.RemoveAsync(entity);
        }
        public async Task<IEnumerable<EventParticipantDto>> GetByUserIdAsync(string userId)
        {
            var userEvents = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<EventParticipantDto>>(userEvents);
        }


    }
}
