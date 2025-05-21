using JoinTheFun.BLL.DTO.Posts;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Отримати всі пости
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Отримати пост за ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        /// <summary>
        /// Створити новий пост
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            await _postService.CreateAsync(dto);
            return Ok(new { message = "Пост створено успішно" });
        }

        /// <summary>
        /// Видалити пост за ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeleteAsync(id);
            return Ok(new { message = "Пост видалено успішно" });
        }

        [HttpGet("following/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByFollowings(string userId)
        {
            var posts = await _postService.GetPostsByFollowingsAsync(userId);
            return Ok(posts);
        }

    }
}
