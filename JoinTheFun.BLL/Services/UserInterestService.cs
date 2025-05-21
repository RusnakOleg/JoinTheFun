using AutoMapper;
using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.DTO.UserInterest;
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
    public class UserInterestService : IUserInterestService
    {
        private readonly IUserInterestRepository _repo;
        private readonly IMapper _mapper;

        public UserInterestService(IUserInterestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterestDto>> GetByProfileIdAsync(int profileId)
        {
            var interests = await _repo.GetInterestsByProfileIdAsync(profileId);
            return _mapper.Map<IEnumerable<InterestDto>>(interests);
        }

        public async Task AddAsync(AddUserInterestDto dto)
        {
            var entity = _mapper.Map<UserInterest>(dto);
            await _repo.AddAsync(entity);
        }

        public async Task RemoveAsync(AddUserInterestDto dto)
        {
            var entity = _mapper.Map<UserInterest>(dto);
            await _repo.RemoveAsync(entity);
        }
    }
}
