using JoinTheFun.DAL.Context;
using JoinTheFun.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace JoinTheFun.API.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            // 1. Користувачі
            var anna = await userManager.FindByNameAsync("anna");
            if (anna == null)
            {
                anna = new ApplicationUser
                {
                    UserName = "anna",
                    Email = "anna@email.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(anna, "Anna123!");
            }

            var oleg = await userManager.FindByNameAsync("oleg");
            if (oleg == null)
            {
                oleg = new ApplicationUser
                {
                    UserName = "oleg",
                    Email = "oleg@email.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(oleg, "Oleg123!");
            }

            // 2. Профілі
            if (!context.Profiles.Any())
            {
                var profileAnna = new Profile
                {
                    UserId = anna.Id,
                    Age = 25,
                    City = "Lviv",
                    Description = "Люблю фото та каву",
                    AvatarUrl = "mmm",
                    Gender = Gender.Female
                };

                var profileOleg = new Profile
                {
                    UserId = oleg.Id,
                    Age = 28,
                    City = "Kyiv",
                    Description = "Геймер і турист",
                    AvatarUrl = "mmm",
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
                    ImageUrl = "mmm",
                    CreatedAt = DateTime.UtcNow
                };
                var post2 = new Post
                {
                    UserId = oleg.Id,
                    Content = "Граємо в CS GO — хто з нами?",
                    ImageUrl = "mmm",
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
