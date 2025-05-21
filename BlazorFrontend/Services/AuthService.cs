using System.Net.Http.Json;
using Blazored.LocalStorage;
using BlazorFrontend.DTO.Login;
using BlazorFrontend.DTO.Register;
using BlazorFrontend.Services.Auth;
using BlazorFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorFrontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider provider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = (CustomAuthStateProvider)provider;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var response = await _http.PostAsJsonAsync("auth/register", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (result != null)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                _authStateProvider.NotifyUserAuthentication(result.Token);
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
            }

            return result;
        }
    }
}
