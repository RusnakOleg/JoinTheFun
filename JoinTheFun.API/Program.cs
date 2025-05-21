using JoinTheFun.API.Data;
using JoinTheFun.DAL.Context;
using JoinTheFun.DAL.Entities;
using JoinTheFun.DAL.Repositories.Interfaces;
using JoinTheFun.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JoinTheFun.BLL.Mapping;
using JoinTheFun.BLL.Services.Interfaces;
using JoinTheFun.BLL.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== DB Conection =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ===== DAL Repositories =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
builder.Services.AddScoped<IUserInterestRepository, UserInterestRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostLikeRepository, PostLikeRepository>();
builder.Services.AddScoped<IPostCommentRepository, PostCommentRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();

// ===== AutoMapper =====
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ===== BLL Services =====
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventParticipantService, EventParticipantService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostLikeService, PostLikeService>();
builder.Services.AddScoped<IPostCommentService, PostCommentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<IUserInterestService, UserInterestService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IAuthService, AuthService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    await DataSeeder.SeedAsync(userManager, roleManager, dbContext);
}


app.Run();
