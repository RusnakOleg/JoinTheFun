using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestsController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        /// <summary>
        /// Отримати всі інтереси
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestDto>>> GetAll()
        {
            var interests = await _interestService.GetAllAsync();
            return Ok(interests);
        }

        /// <summary>
        /// Додати новий інтерес
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInterestDto dto)
        {
            await _interestService.CreateAsync(dto);
            return Ok(new { message = "Інтерес створено" });
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _interestService.DeleteAsync(id);
            return Ok(new { message = "Інтерес видалено" });
        }
    }
}
