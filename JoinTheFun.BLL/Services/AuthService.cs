using JoinTheFun.BLL.DTO.Login;
using JoinTheFun.BLL.DTO.Register;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepo;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, IProfileRepository profileRepo, IConfiguration config)
        {
            _userManager = userManager;
            _profileRepo = profileRepo;
            _config = config;
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

            // При реєстрації за замовчуванням даємо роль "User"
            await _userManager.AddToRoleAsync(user, "User");

            // створення порожнього профілю
            var profile = new Profile
            {
                UserId = user.Id,
                Description = "",
                City = "",
                Age = 0,
                Gender = Gender.Male,
                AvatarUrl = Array.Empty<byte>()
            };

            await _profileRepo.AddAsync(profile);
            return true;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;
            
            //  Перевіряємо, чи користувач забанений
            if (await _userManager.IsLockedOutAsync(user))
            {
                throw new Exception("Ваш акаунт заблоковано адміністратором.");
            }
            
            var token = await GenerateJwtTokenAsync(user); 

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Username = user.UserName
            };
        }
        
        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            // Отримуємо всі ролі користувача з бази даних
            var roles = await _userManager.GetRolesAsync(user);

            // Додаємо кожну роль у claims токена
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims, // Передаємо наш список ліста claims
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}