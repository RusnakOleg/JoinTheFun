using Bogus;
using JoinTheFun.DAL.Context;
using JoinTheFun.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoinTheFun.API.Data
{
    public static class DataSeeder
    {
        // Набори реальних українських слів та фраз для генерації
        private static readonly string[] UkrWords = {
            "привіт", "фестиваль", "зустріч", "кава", "програмування", "дизайн", "вечірка", "спорт", 
            "прогулянка", "кіно", "музика", "книга", "спільнота", "розробка", "ідея", "проєкт", 
            "мистецтво", "фотографія", "подорож", "активність", "відпочинок", "друзі", "натхнення"
        };

        private static readonly string[] UkrSentences = {
            "Чудовий день для того, щоб дізнатися щось нове та корисне.",
            "Зустрічаємось у центрі міста біля головного входу.",
            "Не забудьте взяти з собою гарний настрій та друзів!",
            "Обговорюємо нові тренди у сфері сучасних технологій.",
            "Практичний воркшоп для всіх, хто хоче розвиватися.",
            "Приєднуйтесь до нашої великої та дружньої команди.",
            "Ділимося досвідом, п'ємо смачну каву та спілкуємося.",
            "Кількість місць обмежена, тому реєструйтеся заздалегідь.",
            "Сьогодні був неймовірний день, повний яскравих емоцій та нових знайомств."
        };

        private static readonly string[] EventTitles = {
            "Воркшоп з веб-дизайну та UI/UX",
            "Кіновечір просто неба",
            "Благодійний забіг ради перемоги",
            "ІТ-мітап: Тренди розробки 2026",
            "Фотопрогулянка старим містом",
            "Турнір з настільних ігор",
            "Музичний джем-сейшн",
            "Літературний клуб: обговорення новинок",
            "Йога-пікнік у парку",
            "Майстер-клас з живопису",
            "Хакатон для молодих розробників"
        };

        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
           

            var fakerUk = new Faker("uk"); 
            var fakerEn = new Faker("en"); 

            // Функції-помічники для генерації українського тексту замість зламаного Lorem
            string GetUkrSentence() => fakerUk.PickRandom(UkrSentences);
            string GetUkrParagraph(int count = 3) => string.Join(" ", Enumerable.Range(0, count).Select(_ => GetUkrSentence()));

            // =========================================
            // ROLES
            // =========================================
            string[] roles = { "Admin", "Moderator", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // =========================================
            // ADMIN
            // =========================================
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@jointhefun.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");

            // =========================================
            // USERS
            // =========================================
            var users = new List<ApplicationUser>();
            for (int i = 0; i < 20; i++)
            {
                var username = fakerEn.Internet.UserName()
                    .Replace(".", "_")
                    .ToLower();

                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = fakerEn.Internet.Email(username),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    users.Add(user);
                }
            }
            users.Add(admin);

            // =========================================
            // INTERESTS
            // =========================================
            var interests = new List<Interest>
            {
                new() { Name = "Фотографія" },
                new() { Name = "Геймінг" },
                new() { Name = "Подорожі" },
                new() { Name = "Фітнес" },
                new() { Name = "Програмування" },
                new() { Name = "Кіно" },
                new() { Name = "Музика" },
                new() { Name = "Книги" }
            };
            context.Interests.AddRange(interests);
            await context.SaveChangesAsync();

            // =========================================
            // PROFILES
            // =========================================
            var cities = new[] { "Київ", "Львів", "Чернівці", "Одеса", "Харків", "Дніпро", "Івано-Франківськ", "Тернопіль" };
            var profiles = new List<Profile>();
            int avatarIndex = 1;

            foreach (var user in users)
            {
                var profile = new Profile
                {
                    UserId = user.Id,
                    Age = fakerUk.Random.Int(18, 35),
                    City = fakerUk.PickRandom(cities),
                    Description = GetUkrSentence(), // Справжнє речення українською

                    AvatarUrl = await GetImageBytesAsync($"https://i.pravatar.cc/300?img={avatarIndex}"),
                    Gender = fakerUk.PickRandom<Gender>()
                };

                avatarIndex++;
                profiles.Add(profile);
            }
            context.Profiles.AddRange(profiles);
            await context.SaveChangesAsync();

            // =========================================
            // USER INTERESTS
            // =========================================
            var userInterests = new List<UserInterest>();
            foreach (var profile in profiles)
            {
                var selectedInterests = interests
                    .OrderBy(x => Guid.NewGuid())
                    .Take(fakerUk.Random.Int(2, 4))
                    .ToList();

                foreach (var interest in selectedInterests)
                {
                    userInterests.Add(new UserInterest
                    {
                        ProfileId = profile.Id,
                        InterestId = interest.InterestId
                    });
                }
            }
            context.UserInterests.AddRange(userInterests);
            await context.SaveChangesAsync();

            // =========================================
            // POSTS
            // =========================================
            var posts = new List<Post>();
            int imageIndex = 1;

            foreach (var user in users)
            {
                int count = fakerUk.Random.Int(2, 5);
                for (int i = 0; i < count; i++)
                {
                    byte[] imageBytes = Array.Empty<byte>();
                    if (fakerUk.Random.Bool(0.6f))
                    {
                        imageBytes = await GetImageBytesAsync($"https://picsum.photos/600/400?random={imageIndex}");
                        imageIndex++;
                    }

                    posts.Add(new Post
                    {
                        UserId = user.Id,
                        Content = GetUkrParagraph(2), // Параграф українською
                        ImageUrl = imageBytes,
                        CreatedAt = fakerUk.Date.Recent(30)
                    });
                }
            }
            context.Posts.AddRange(posts);
            await context.SaveChangesAsync();

            // =========================================
            // COMMENTS
            // =========================================
            var comments = new List<PostComment>();
            foreach (var post in posts)
            {
                var randomUsers = users.OrderBy(x => Guid.NewGuid()).Take(fakerUk.Random.Int(1, 5));
                foreach (var user in randomUsers)
                {
                    comments.Add(new PostComment
                    {
                        PostId = post.PostId,
                        UserId = user.Id,
                        Content = GetUkrSentence(), // Коментар українською
                        CreatedAt = fakerUk.Date.Recent(20)
                    });
                }
            }
            context.PostComments.AddRange(comments);
            await context.SaveChangesAsync();

            // =========================================
            // LIKES (без змін)
            // =========================================
            var likes = new List<PostLike>();
            foreach (var post in posts)
            {
                var likedUsers = users.OrderBy(x => Guid.NewGuid()).Take(fakerUk.Random.Int(1, 10));
                foreach (var user in likedUsers)
                {
                    if (!likes.Any(x => x.PostId == post.PostId && x.UserId == user.Id))
                    {
                        likes.Add(new PostLike { PostId = post.PostId, UserId = user.Id });
                    }
                }
            }
            context.PostLikes.AddRange(likes);
            await context.SaveChangesAsync();

            // =========================================
            // EVENTS
            // =========================================
            var eventsList = new List<Event>();
            for (int i = 0; i < 10; i++)
            {
                var creator = fakerUk.PickRandom(users);

                eventsList.Add(new Event
                {
                    Title = fakerUk.PickRandom(EventTitles), // Гарна українська назва
                    Description = GetUkrParagraph(3), // Опис події українською
                    CreatorId = creator.Id,
                    Location = fakerUk.PickRandom(cities),
                    StartTime = fakerUk.Date.Soon(30),
                    CreatedAt = fakerUk.Date.Recent(10),
                    ImageUrl = $"https://picsum.photos/800/500?random=event{i}"
                });
            }
            context.Events.AddRange(eventsList);
            await context.SaveChangesAsync();

            // =========================================
            // EVENT PARTICIPANTS & FOLLOWS (без змін)
            // =========================================
            var participants = new List<EventParticipant>();
            string[] statuses = { "піду", "цікавить" };

            foreach (var ev in eventsList)
            {
                var randomUsers = users.OrderBy(x => Guid.NewGuid()).Take(fakerUk.Random.Int(3, 10));
                foreach (var user in randomUsers)
                {
                    participants.Add(new EventParticipant
                    {
                        EventId = ev.EventId,
                        UserId = user.Id,
                        Status = fakerUk.PickRandom(statuses)
                    });
                }
            }
            context.EventParticipants.AddRange(participants);

            var follows = new List<Follow>();
            foreach (var user in users)
            {
                var following = users.Where(x => x.Id != user.Id).OrderBy(x => Guid.NewGuid()).Take(fakerUk.Random.Int(2, 7));
                foreach (var followUser in following)
                {
                    if (!follows.Any(x => x.FollowerId == user.Id && x.FollowedId == followUser.Id))
                    {
                        follows.Add(new Follow { FollowerId = user.Id, FollowedId = followUser.Id });
                    }
                }
            }
            context.Follows.AddRange(follows);

            await context.SaveChangesAsync();
        }

        private static async Task<byte[]> GetImageBytesAsync(string url)
        {
            using var httpClient = new HttpClient();
            try { return await httpClient.GetByteArrayAsync(url); }
            catch { return Array.Empty<byte>(); }
        }
    }
}