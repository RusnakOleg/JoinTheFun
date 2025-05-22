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
        private readonly IUserInterestRepository _userInterestRepo;
        private readonly IMapper _mapper;

        public ProfileService(
            IProfileRepository repo,
            IUserInterestRepository userInterestRepo,
            IMapper mapper)
        {
            _repo = repo;
            _userInterestRepo = userInterestRepo;
            _mapper = mapper;
        }

        public async Task<ProfileDto?> GetByUserIdAsync(string userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task<IEnumerable<ProfileDto>> GetByCityAsync(string city)
        {
            var profiles = await _repo.GetByCityAsync(city);
            return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
        }

        public async Task<IEnumerable<ProfileDto>> GetByInterestIdAsync(int interestId)
        {
            var profiles = await _repo.GetByInterestIdAsync(interestId);
            return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
        }

        public async Task AddAsync(UpdateProfileDto dto, string userId)
        {
            var profile = _mapper.Map<JoinTheFun.DAL.Entities.Profile>(dto);
            profile.UserId = userId;

            await _repo.AddAsync(profile);
        }

        public async Task UpdateAsync(UpdateProfileDto dto, string userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            if (profile == null) return;

            _mapper.Map(dto, profile);
            await _repo.UpdateAsync(profile);

            // 🔴 Видаляємо старі інтереси
            var existing = await _userInterestRepo.GetInterestsByProfileIdAsync(profile.Id);
            foreach (var oldInterest in existing)
            {
                await _userInterestRepo.RemoveAsync(oldInterest);
            }

            // 🟢 Додаємо нові
            foreach (var interestId in dto.InterestIds)
            {
                var newInterest = new DAL.Entities.UserInterest
                {
                    ProfileId = profile.Id,
                    InterestId = interestId
                };
                await _userInterestRepo.AddAsync(newInterest);
            }
        }


        public async Task<IEnumerable<ProfileDto>> GetAllAsync()
        {
            var profiles = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
        }

    }

}
