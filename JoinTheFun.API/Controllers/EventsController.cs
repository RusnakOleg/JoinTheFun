using JoinTheFun.BLL.DTO.Events;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Отримати всі події
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        /// <summary>
        /// Отримати події за містом
        /// </summary>
        [HttpGet("by-location")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetByLocation([FromQuery] string city)
        {
            var events = await _eventService.GetByLocationAsync(city);
            return Ok(events);
        }

        /// <summary>
        /// Отримати події, що відповідають інтересам користувача
        /// </summary>
        [HttpGet("by-user-interests")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetByUserInterests([FromQuery] string userId)
        {
            var events = await _eventService.GetByUserInterestsAsync(userId);
            return Ok(events);
        }

        /// <summary>
        /// Створити нову подію
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            await _eventService.CreateAsync(dto);
            return Ok(new { message = "Подія створена успішно" });
        }
    }
}
