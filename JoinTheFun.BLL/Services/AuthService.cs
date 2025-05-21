using JoinTheFun.BLL.DTO.Register;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepo;

        public AuthService(UserManager<ApplicationUser> userManager, IProfileRepository profileRepo)
        {
            _userManager = userManager;
            _profileRepo = profileRepo;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Username
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return false;

            // створення порожнього профілю
            var profile = new Profile
            {
                UserId = user.Id,
                Description = "",
                City = "",
                Age = 0,
                Gender = Gender.Male, // або Female — за замовчуванням
                AvatarUrl = ""
            };

            await _profileRepo.AddAsync(profile);
            return true;
        }
    }

}
