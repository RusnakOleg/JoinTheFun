using System.Security.Claims;
using JoinTheFun.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace JoinTheFun.API.Middleware
{
    public class UserBanCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public UserBanCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            // Якщо користувач автентифікований (надіслав токен)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // Витягуємо ID користувача з токена (Sub або NameIdentifier)
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                             ?? context.User.FindFirst("sub")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await userManager.FindByIdAsync(userId);
                    
                    // Якщо користувача видалили або він заблокований
                    if (user == null || await userManager.IsLockedOutAsync(user))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsJsonAsync(new { message = "Ваш акаунт заблоковано або видалено." });
                        return; // Зупиняємо запит, далі він не йде
                    }
                }
            }

            await _next(context);
        }
    }
}