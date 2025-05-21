using JoinTheFun.BLL.DTO.Comments;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IPostCommentService _commentService;

        public CommentsController(IPostCommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Отримати всі коментарі до поста
        /// </summary>
        [HttpGet("by-post/{postId}")]
        public async Task<ActionResult<IEnumerable<PostCommentDto>>> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        /// <summary>
        /// Додати коментар до поста
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostCommentDto dto)
        {
            await _commentService.AddAsync(dto);
            return Ok(new { message = "Коментар додано" });
        }

        /// <summary>
        /// Видалити коментар за ID
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            await _commentService.DeleteAsync(commentId);
            return Ok(new { message = "Коментар видалено" });
        }
    }
}
