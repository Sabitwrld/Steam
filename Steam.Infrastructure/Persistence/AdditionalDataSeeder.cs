using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Achievements;
using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Identity;
using Steam.Domain.Entities.ReviewsRating;
using Steam.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Persistence
{
    public static class AdditionalDataSeeder
    {
        public static async Task SeedAdditionalDataAsync(AppDbContext context, UserManager<AppUser> userManager)
        {
            if (await context.Applications.AnyAsync())
            {
                return;
            }

            // --- 1. JANRLAR VƏ ETİKETLƏR ---
            var genres = new List<Genre>
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Simulation" },
                new Genre { Name = "Horror" }
            };
            await context.Genres.AddRangeAsync(genres);

            var tags = new List<Tag>
            {
                new Tag { Name = "Multiplayer" },
                new Tag { Name = "Singleplayer" },
                new Tag { Name = "Co-op" },
                new Tag { Name = "Open World" },
                new Tag { Name = "FPS" },
                new Tag { Name = "Story Rich" },
                new Tag { Name = "Cyberpunk" }
            };
            await context.Tags.AddRangeAsync(tags);
            await context.SaveChangesAsync();

            // --- 2. NÜMUNƏ İSTİFADƏÇİLƏR ---
            var user1 = new AppUser { FullName = "Ali Valiyev", UserName = "ali", Email = "ali@example.com", EmailConfirmed = true };
            var user2 = new AppUser { FullName = "Nigar Memmedova", UserName = "nigar", Email = "nigar@example.com", EmailConfirmed = true };
            await userManager.CreateAsync(user1, "Ali123!");
            await userManager.CreateAsync(user2, "Nigar123!");
            await userManager.AddToRoleAsync(user1, "User");
            await userManager.AddToRoleAsync(user2, "User");

            // --- 3. OYUNLAR (ApplicationCatalog) ---
            var cyberpunkGame = new ApplicationCatalog
            {
                Name = "Cyberpunk 2077",
                Description = "An open-world, action-adventure RPG set in the metropolis of Night City, where you play as a cyberpunk mercenary wrapped up in a do-or-die fight for survival.",
                ReleaseDate = new DateTime(2020, 12, 10),
                Developer = "CD PROJEKT RED",
                Publisher = "CD PROJEKT RED",
                ApplicationType = "Game",
                Genres = new List<Genre> { genres.Single(g => g.Name == "Action"), genres.Single(g => g.Name == "RPG") },
                Tags = new List<Tag> { tags.Single(t => t.Name == "Singleplayer"), tags.Single(t => t.Name == "Open World"), tags.Single(t => t.Name == "Cyberpunk") }
            };

            var witcherGame = new ApplicationCatalog
            {
                Name = "The Witcher 3: Wild Hunt",
                Description = "A story-driven, next-generation open world role-playing game, set in a visually stunning fantasy universe full of meaningful choices and impactful consequences.",
                ReleaseDate = new DateTime(2015, 5, 19),
                Developer = "CD PROJEKT RED",
                Publisher = "CD PROJEKT RED",
                ApplicationType = "Game",
                Genres = new List<Genre> { genres.Single(g => g.Name == "Adventure"), genres.Single(g => g.Name == "RPG") },
                Tags = new List<Tag> { tags.Single(t => t.Name == "Singleplayer"), tags.Single(t => t.Name == "Open World"), tags.Single(t => t.Name == "Story Rich") }
            };

            var csgoGame = new ApplicationCatalog
            {
                Name = "Counter-Strike 2",
                Description = "For over two decades, millions of players globally have battled it out in a game that requires skill, strategy, and teamwork. Now the next chapter in the CS story is about to begin. This is Counter-Strike 2.",
                ReleaseDate = new DateTime(2023, 9, 27),
                Developer = "Valve",
                Publisher = "Valve",
                ApplicationType = "Game",
                Genres = new List<Genre> { genres.Single(g => g.Name == "Action") },
                Tags = new List<Tag> { tags.Single(t => t.Name == "Multiplayer"), tags.Single(t => t.Name == "FPS") }
            };

            await context.Applications.AddRangeAsync(cyberpunkGame, witcherGame, csgoGame);
            await context.SaveChangesAsync();

            // --- 4. MEDIA (ŞƏKİL VƏ VİDEOLAR) ---
            var mediaItems = new List<Media>
            {
                // Cyberpunk 2077 Media
                new Media { ApplicationId = cyberpunkGame.Id, Url = "https://cdn.gracza.pl/i_gp/h/22/346431843.jpg", MediaType = "Image", Order = 1 },
                new Media { ApplicationId = cyberpunkGame.Id, Url = "https://www.youtube.com/watch?v=8X2kIfS6fb8", MediaType = "Video", Order = 2 },
                new Media { ApplicationId = cyberpunkGame.Id, Url = "https://www.cyberpunk.net/build/images/media/screenshots/screenshot-1-en-a8080f63.jpg", MediaType = "Image", Order = 3 },
                new Media { ApplicationId = cyberpunkGame.Id, Url = "https://www.cyberpunk.net/build/images/media/screenshots/screenshot-2-en-fd066a3c.jpg", MediaType = "Image", Order = 4 },
                
                // The Witcher 3 Media
                new Media { ApplicationId = witcherGame.Id, Url = "https://cdn.gracza.pl/i_gp/h/13/353401531.jpg", MediaType = "Image", Order = 1 },
                new Media { ApplicationId = witcherGame.Id, Url = "https://www.youtube.com/watch?v=c0i88t0Kacs", MediaType = "Video", Order = 2 },
                new Media { ApplicationId = witcherGame.Id, Url = "https://www.thewitcher.com/en/witcher3/P/media/--preset-standard/assets/images/297298642a8b341f71a9f0e154f85e50529d892d-2200x1238.jpg", MediaType = "Image", Order = 3 },
               
                // Counter-Strike 2 Media
                new Media { ApplicationId = csgoGame.Id, Url = "https://cdn.akamai.steamstatic.com/apps/csgo/images/csgo_react/social/cs2.jpg", MediaType = "Image", Order = 1 },
                new Media { ApplicationId = csgoGame.Id, Url = "https://www.youtube.com/watch?v=c80_weOc_1k", MediaType = "Video", Order = 2 }
            };
            await context.Media.AddRangeAsync(mediaItems);

            // --- 5. SİSTEM TƏLƏBLƏRİ ---
            var systemRequirements = new List<SystemRequirements>
            {
                new SystemRequirements { ApplicationId = cyberpunkGame.Id, RequirementType = "Minimum", OS = "Windows 10", CPU = "Intel Core i7-6700 or AMD Ryzen 5 1600", GPU = "NVIDIA GeForce GTX 1060 6GB", RAM = "12 GB", Storage = "70 GB SSD" },
                new SystemRequirements { ApplicationId = cyberpunkGame.Id, RequirementType = "Recommended", OS = "Windows 10", CPU = "Intel Core i7-12700 or AMD Ryzen 7 7800X3D", GPU = "NVIDIA GeForce RTX 2060 SUPER", RAM = "16 GB", Storage = "70 GB SSD" },
                new SystemRequirements { ApplicationId = witcherGame.Id, RequirementType = "Minimum", OS = "Windows 7/8 (8.1) 64-bit", CPU = "Intel CPU Core i5-2500K 3.3GHz", GPU = "Nvidia GPU GeForce GTX 660", RAM = "6 GB", Storage = "35 GB" },
                new SystemRequirements { ApplicationId = csgoGame.Id, RequirementType = "Minimum", OS = "Windows 10", CPU = "Intel® Core™ i5 750 or higher", GPU = "Video card must be 1 GB or more", RAM = "8 GB", Storage = "85 GB" },
            };
            await context.SystemRequirements.AddRangeAsync(systemRequirements);

            // --- 6. QİYMƏTLƏR ---
            var pricePoints = new List<PricePoint>
            {
                new PricePoint { ApplicationId = cyberpunkGame.Id, Name = "Standard Edition", BasePrice = 59.99m },
                new PricePoint { ApplicationId = witcherGame.Id, Name = "Game of the Year Edition", BasePrice = 49.99m },
                new PricePoint { ApplicationId = csgoGame.Id, Name = "Prime Status Upgrade", BasePrice = 14.99m }
            };
            await context.PricePoints.AddRangeAsync(pricePoints);
            await context.SaveChangesAsync(); // PricePoint ID-ləri yaransın

            // --- 7. RƏYLƏR ---
            var reviews = new List<Review>
            {
                new Review { UserId = user1.Id, ApplicationId = cyberpunkGame.Id, Title = "Amazing Game!", Content = "Night City is a breathtaking world to explore. The story is immersive and the characters are unforgettable. A must-play for any RPG fan.", IsRecommended = true, HelpfulCount = 15, FunnyCount = 2 },
                new Review { UserId = user2.Id, ApplicationId = witcherGame.Id, Title = "A Masterpiece", Content = "The Witcher 3 is one of the best games I have ever played. The world is huge and full of interesting quests. The story is simply incredible.", IsRecommended = true, HelpfulCount = 25 },
                new Review { UserId = user1.Id, ApplicationId = witcherGame.Id, Title = "Can't stop playing", Content = "Even years after its release, this game is still better than most new titles. The level of detail is just insane.", IsRecommended = true, HelpfulCount = 5 },
            };
            await context.Reviews.AddRangeAsync(reviews);

            // --- 8. NAİLİYYƏTLƏR (ACHIEVEMENTS) ---
            var achievements = new List<Achievement>
            {
                // Cyberpunk 2077
                new Achievement { ApplicationId = cyberpunkGame.Id, Name = "The Fool", Description = "Become a mercenary.", Points = 10, IconUrl = "https://www.trueachievements.com/imagestore/0004128900/4128978.jpg"},
                new Achievement { ApplicationId = cyberpunkGame.Id, Name = "The Lovers", Description = "Steal the Relic.", Points = 20, IconUrl = "https://www.trueachievements.com/imagestore/0004128900/4128979.jpg"},
                // The Witcher 3
                new Achievement { ApplicationId = witcherGame.Id, Name = "Lilac and Gooseberries", Description = "Find Yennefer of Vengerberg.", Points = 15, IconUrl = "https://www.trueachievements.com/imagestore/0001594900/1594982.jpg" },
                new Achievement { ApplicationId = witcherGame.Id, Name = "Dendrologist", Description = "Acquire all the Abilities in one tree.", Points = 30, IconUrl = "https://www.trueachievements.com/imagestore/0001594900/1594998.jpg" }
            };
            await context.Set<Achievement>().AddRangeAsync(achievements);

            await context.SaveChangesAsync();
        }
    }
}
