using AutoMapper;
using JoinTheFun.BLL.DTO.Interests;
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
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _repo;
        private readonly IMapper _mapper;

        public InterestService(IInterestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterestDto>> GetAllAsync()
        {
            var interests = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<InterestDto>>(interests);
        }

        public async Task CreateAsync(CreateInterestDto dto)
        {
            var interest = _mapper.Map<Interest>(dto);
            // Перевірку на унікальність можеш додати тут
            await _repo.AddAsync(interest);
        }
    }
}
