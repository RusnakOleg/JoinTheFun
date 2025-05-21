using JoinTheFun.BLL.DTO.Profiles;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetAll()
        {
            var profiles = await _profileService.GetAllAsync();
            return Ok(profiles);
        }

        /// <summary>
        /// Отримати профіль користувача
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<ActionResult<ProfileDto>> GetByUserId(string userId)
        {
            var profile = await _profileService.GetByUserIdAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        /// <summary>
        /// Отримати профілі за містом
        /// </summary>
        [HttpGet("by-city")]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetByCity([FromQuery] string city)
        {
            var profiles = await _profileService.GetByCityAsync(city);
            return Ok(profiles);
        }

        /// <summary>
        /// Отримати профілі за інтересом
        /// </summary>
        [HttpGet("by-interest")]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetByInterestId([FromQuery] int interestId)
        {
            var profiles = await _profileService.GetByInterestIdAsync(interestId);
            return Ok(profiles);
        }

        /// <summary>
        /// Створити профіль (одноразово при реєстрації)
        /// </summary>
        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(string userId, [FromBody] UpdateProfileDto dto)
        {
            await _profileService.AddAsync(dto, userId);
            return Ok(new { message = "Профіль створено" });
        }

        /// <summary>
        /// Оновити профіль
        /// </summary>
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateProfileDto dto)
        {
            await _profileService.UpdateAsync(dto, userId);
            return Ok(new { message = "Профіль оновлено" });
        }
    }
}
