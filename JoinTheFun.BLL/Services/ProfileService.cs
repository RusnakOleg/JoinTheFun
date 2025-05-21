using AutoMapper;
using JoinTheFun.BLL.DTO.Profiles;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repo;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProfileDto?> GetByUserIdAsync(string userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task UpdateAsync(string userId, UpdateProfileDto dto)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            _mapper.Map(dto, profile);
            await _repo.UpdateAsync(profile);
        }
    }
}
