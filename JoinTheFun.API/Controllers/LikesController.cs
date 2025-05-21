using JoinTheFun.BLL.DTO.Likes;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IPostLikeService _likeService;

        public LikesController(IPostLikeService likeService)
        {
            _likeService = likeService;
        }

        /// <summary>
        /// Перевірити, чи користувач лайкнув пост
        /// </summary>
        [HttpGet("is-liked")]
        public async Task<ActionResult<bool>> IsLiked([FromQuery] int postId, [FromQuery] string userId)
        {
            var liked = await _likeService.IsLikedAsync(postId, userId);
            return Ok(liked);
        }

        /// <summary>
        /// Лайкнути пост
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Like([FromBody] PostLikeDto dto)
        {
            await _likeService.LikeAsync(dto);
            return Ok(new { message = "Лайк додано" });
        }

        /// <summary>
        /// Видалити лайк
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Unlike([FromBody] PostLikeDto dto)
        {
            await _likeService.UnlikeAsync(dto);
            return Ok(new { message = "Лайк видалено" });
        }
    }
}
