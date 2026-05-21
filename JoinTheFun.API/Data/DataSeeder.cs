using JoinTheFun.DAL.Context;
using JoinTheFun.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoinTheFun.API.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            // 0. Створення ролей (Новий блок)
            string[] roleNames = { "Admin", "Moderator", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 1. Користувачі
            var anna = await userManager.FindByNameAsync("anna_shevchenko");
            if (anna == null)
            {
                anna = new ApplicationUser
                {
                    UserName = "anna_shevchenko",
                    Email = "anna_shevchenko@email.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(anna, "Anna123!");
                
                // Надаємо Анні роль звичайного користувача
                await userManager.AddToRoleAsync(anna, "User");
            }

            var oleg = await userManager.FindByNameAsync("oleg_rusnak");
            if (oleg == null)
            {
                oleg = new ApplicationUser
                {
                    UserName = "oleg_rusnak",
                    Email = "oleg_rusnak@email.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(oleg, "Oleg123!");
                
                // Робимо Олега Адміністратором додатка
                await userManager.AddToRoleAsync(oleg, "Admin");
            }

            // 2. Профілі (Твоя існуюча логіка без змін)
            if (!context.Profiles.Any())
            {
                var profileAnna = new Profile
                {
                    UserId = anna.Id,
                    Age = 25,
                    City = "Львів",
                    Description = "Люблю фото та каву",
                    AvatarUrl = Array.Empty<byte>(),
                    Gender = Gender.Female
                };

                var profileOleg = new Profile
                {
                    UserId = oleg.Id,
                    Age = 28,
                    City = "Київ",
                    Description = "Геймер і турист",
                    AvatarUrl = Array.Empty<byte>(),
                    Gender = Gender.Male
                };

                context.Profiles.AddRange(profileAnna, profileOleg);
                await context.SaveChangesAsync();

                // 3. Інтереси
                if (!context.Interests.Any())
                {
                    var photo = new Interest { Name = "Фотографія" };
                    var gaming = new Interest { Name = "Геймінг" };
                    var tourism = new Interest { Name = "Туризм" };

                    context.Interests.AddRange(photo, gaming, tourism);
                    await context.SaveChangesAsync();

                    // 4. UserInterests
                    context.UserInterests.AddRange(
                        new UserInterest { ProfileId = profileAnna.Id, InterestId = photo.InterestId },
                        new UserInterest { ProfileId = profileOleg.Id, InterestId = gaming.InterestId },
                        new UserInterest { ProfileId = profileOleg.Id, InterestId = tourism.InterestId });
                    await context.SaveChangesAsync();
                }

                // 5. Пости
                var post1 = new Post
                {
                    UserId = anna.Id,
                    Content = "Крута прогулянка по Карпатах!",
                    ImageUrl = Array.Empty<byte>(),
                    CreatedAt = DateTime.UtcNow
                };
                var post2 = new Post
                {
                    UserId = oleg.Id,
                    Content = "Граємо в CS GO — хто з нами?",
                    ImageUrl = Array.Empty<byte>(),
                    CreatedAt = DateTime.UtcNow
                };

                context.Posts.AddRange(post1, post2);
                await context.SaveChangesAsync();

                // 6. Коментарі
                context.PostComments.AddRange(
                    new PostComment
                    {
                        PostId = post1.PostId,
                        UserId = oleg.Id,
                        Content = "Вау! Де саме були?",
                        CreatedAt = DateTime.UtcNow
                    },
                    new PostComment
                    {
                        PostId = post2.PostId,
                        UserId = anna.Id,
                        Content = "Я з вами!",
                        CreatedAt = DateTime.UtcNow
                    });
                await context.SaveChangesAsync();

                // 7. Лайки
                context.PostLikes.AddRange(
                    new PostLike { PostId = post1.PostId, UserId = oleg.Id },
                    new PostLike { PostId = post2.PostId, UserId = anna.Id });
                await context.SaveChangesAsync();

                // 8. Події
                var photoEvent = new Event
                {
                    Title = "Фото-прогулянка у Львові",
                    Description = "Зустрічаємось біля Оперного",
                    CreatorId = anna.Id,
                    Location = "Львів",
                    StartTime = DateTime.UtcNow.AddDays(5),
                    ImageUrl = "mmm",
                    CreatedAt = DateTime.UtcNow
                };

                context.Events.Add(photoEvent);
                await context.SaveChangesAsync();

                // 9. Участь
                context.EventParticipants.AddRange(
                    new EventParticipant { EventId = photoEvent.EventId, UserId = anna.Id, Status = "going" },
                    new EventParticipant { EventId = photoEvent.EventId, UserId = oleg.Id, Status = "interested" });
                await context.SaveChangesAsync();

                // 10. Follow
                context.Follows.Add(new Follow
                {
                    FollowerId = oleg.Id,
                    FollowedId = anna.Id
                });
                await context.SaveChangesAsync();
            }
        }
    }
}