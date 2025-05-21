using System.Net.Http.Json;
using BlazorFrontend.DTO.Login;
using BlazorFrontend.DTO.Register;
using BlazorFrontend.Services.Interfaces;

namespace BlazorFrontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var response = await _http.PostAsJsonAsync("auth/register", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("auth/login", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            }

            return null;
        }
    }
}
