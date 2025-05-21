using JoinTheFun.BLL.DTO.Folowers;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        /// <summary>
        /// Перевірити, чи користувач підписаний на іншого
        /// </summary>
        [HttpGet("is-following")]
        public async Task<ActionResult<bool>> IsFollowing([FromQuery] string followerId, [FromQuery] string followedId)
        {
            var isFollowing = await _followService.IsFollowingAsync(followerId, followedId);
            return Ok(isFollowing);
        }

        /// <summary>
        /// Підписатись на користувача
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowDto dto)
        {
            await _followService.FollowAsync(dto);
            return Ok(new { message = "Підписка додана" });
        }

        /// <summary>
        /// Відписатись від користувача
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Unfollow([FromBody] FollowDto dto)
        {
            await _followService.UnfollowAsync(dto);
            return Ok(new { message = "Підписка видалена" });
        }
    }
}
