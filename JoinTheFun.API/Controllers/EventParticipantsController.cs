using JoinTheFun.BLL.DTO.Event_Participants;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventParticipantsController : ControllerBase
    {
        private readonly IEventParticipantService _participantService;

        public EventParticipantsController(IEventParticipantService participantService)
        {
            _participantService = participantService;
        }

        /// <summary>
        /// Отримати всіх учасників події
        /// </summary>
        [HttpGet("{eventId}")]
        public async Task<ActionResult<IEnumerable<EventParticipantDto>>> GetByEventId(int eventId)
        {
            var participants = await _participantService.GetByEventIdAsync(eventId);
            return Ok(participants);
        }

        /// <summary>
        /// Долучити користувача до події (going / interested)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EventParticipantDto dto)
        {
            await _participantService.AddAsync(dto);
            return Ok(new { message = "Участь у події зареєстровано" });
        }

        /// <summary>
        /// Видалити участь користувача
        /// </summary>
        [HttpDelete]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] RemoveEventParticipantDto dto)
        {
            await _participantService.RemoveAsync(dto);
            return Ok(new { message = "Участь у події скасовано" });
        }


    }
}
