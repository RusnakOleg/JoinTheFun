using BlazorFrontend.DTO.Login;
using BlazorFrontend.DTO.Register;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
