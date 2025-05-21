using Blazored.LocalStorage;
using BlazorFrontend.Components;
using BlazorFrontend.Services;
using BlazorFrontend.Services.Auth;
using BlazorFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<IProfileService, ProfileService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IInterestService, InterestService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<ICommentService, CommentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IEventParticipantService, EventParticipantService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IEventService, EventService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IFollowService, FollowService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<ILikeService, LikeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IPostService, PostService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IUserInterestService, UserInterestService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7038/api/");
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
