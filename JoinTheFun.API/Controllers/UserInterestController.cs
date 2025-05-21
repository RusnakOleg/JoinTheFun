using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.DTO.UserInterest;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInterestController : ControllerBase
    {
        private readonly IUserInterestService _userInterestService;

        public UserInterestController(IUserInterestService userInterestService)
        {
            _userInterestService = userInterestService;
        }

        /// <summary>
        /// Отримати всі інтереси профілю
        /// </summary>
        [HttpGet("{profileId}")]
        public async Task<ActionResult<IEnumerable<InterestDto>>> GetByProfileId(int profileId)
        {
            var interests = await _userInterestService.GetByProfileIdAsync(profileId);
            return Ok(interests);
        }

        /// <summary>
        /// Додати інтерес до профілю
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserInterestDto dto)
        {
            await _userInterestService.AddAsync(dto);
            return Ok(new { message = "Інтерес додано до профілю" });
        }

        /// <summary>
        /// Видалити інтерес з профілю
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] AddUserInterestDto dto)
        {
            await _userInterestService.RemoveAsync(dto);
            return Ok(new { message = "Інтерес видалено з профілю" });
        }
    }
}
