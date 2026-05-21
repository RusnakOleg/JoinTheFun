using AutoMapper;
using JoinTheFun.BLL.DTO.Profiles;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JoinTheFun.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepo;
        private readonly IMapper _mapper;

        public AdminService(UserManager<ApplicationUser> userManager, IProfileRepository profileRepo, IMapper mapper)
        {
            _userManager = userManager;
            _profileRepo = profileRepo;
            _mapper = mapper;
        }

        // Отримуємо всіх користувачів для адмінки через існуючий ProfileRepository
        public async Task<IEnumerable<ProfileDto>> GetAllUsersWithProfilesAsync()
        {
            var profiles = await _profileRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
        }

        // Бан користувача через LockoutEnd
        public async Task<bool> BanUserAsync(string userId, int days)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Встановлюємо дату завершення блокування
            var lockoutEndDate = DateTimeOffset.UtcNow.AddDays(days);
            var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEndDate);

            return result.Succeeded;
        }

        // Розблокування користувача
        public async Task<bool> UnbanUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Зкидаємо блокування, передаючи null або минулу дату
            var result = await _userManager.SetLockoutEndDateAsync(user, null);
            return result.Succeeded;
        }
    }
}