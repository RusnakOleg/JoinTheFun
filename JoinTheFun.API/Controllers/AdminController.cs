using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [Authorize(Roles = "Admin")] // Повністю закриваємо контролер для тих, у кого немає ролі Admin
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IPostService _postService;
        private readonly IEventService _eventService;
        private readonly IInterestService _interestService;

        public AdminController(
            IAdminService adminService,
            IPostService postService,
            IEventService eventService,
            IInterestService interestService)
        {
            _adminService = adminService;
            _postService = postService;
            _eventService = eventService;
            _interestService = interestService;
        }

        // --- МОДЕРАЦІЯ КОРИСТУВАЧІВ ---

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersWithProfilesAsync();
            return Ok(users);
        }

        [HttpPost("users/{userId}/ban")]
        public async Task<IActionResult> BanUser(string userId, [FromQuery] int days = 7)
        {
            var result = await _adminService.BanUserAsync(userId, days);
            if (!result) return BadRequest("Не вдалося заблокувати користувача.");

            return Ok(new { message = $"Користувача заблоковано на {days} днів." });
        }

        [HttpPost("users/{userId}/unban")]
        public async Task<IActionResult> UnbanUser(string userId)
        {
            var result = await _adminService.UnbanUserAsync(userId);
            if (!result) return BadRequest("Не вдалося розблокувати користувача.");

            return Ok(new { message = "Користувача успішно розблоковано." });
        }

        // --- МОДЕРАЦІЯ КОНТЕНТУ (Використовуємо твої існуючі методи BLL) ---

        [HttpDelete("posts/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeleteAsync(id);
            return Ok(new { message = "Пост успішно видалено модератором." });
        }

        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteAsync(id);
            return Ok(new { message = "Подію успішно видалено модератором." });
        }

        // --- КЕРУВАННЯ ІНТЕРЕСАМИ ---

        [HttpPost("interests")]
        public async Task<IActionResult> AddInterest([FromBody] CreateInterestDto dto)
        {
            await _interestService.CreateAsync(dto);
            return Ok(new { message = "Новий інтерес успішно додано." });
        }

        [HttpDelete("interests/{id}")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            await _interestService.DeleteAsync(id);
            return Ok(new { message = "Інтерес успішно видалено." });
        }
    }
}