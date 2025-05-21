using JoinTheFun.BLL.DTO.Login;
using JoinTheFun.BLL.DTO.Register;
using JoinTheFun.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinTheFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var success = await _authService.RegisterAsync(dto);

            if (!success)
                return BadRequest(new { message = "Реєстрація не вдалася" });

            return Ok(new { message = "Користувача зареєстровано" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result == null ? Unauthorized() : Ok(result);
        }
    }

}
