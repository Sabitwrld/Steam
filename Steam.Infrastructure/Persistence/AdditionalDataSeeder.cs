using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Identity;
using Steam.Domain.Entities.ReviewsRating;
using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Persistence
{
    public static class AdditionalDataSeeder
    {
        public static async Task SeedAdditionalDataAsync(AppDbContext context, UserManager<AppUser> userManager)
        {
            // Yalnız məlumat bazası boş olduqda toxumlamanı həyata keçirin
            if (await context.Applications.AnyAsync())
            {
                return; // Məlumat bazası artıq toxumlanıb
            }

            // Janrlar və Etiketlər
            var genres = new List<Genre>
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Simulation" }
            };
            await context.Genres.AddRangeAsync(genres);

            var tags = new List<Tag>
            {
                new Tag { Name = "Multiplayer" },
                new Tag { Name = "Singleplayer" },
                new Tag { Name = "Co-op" },
                new Tag { Name = "Open World" },
                new Tag { Name = "FPS" }
            };
            await context.Tags.AddRangeAsync(tags);
            await context.SaveChangesAsync();

            // Nümunə İstifadəçilər
            var user1 = new AppUser { FullName = "Ali Valiyev", UserName = "ali", Email = "ali@example.com", EmailConfirmed = true };
            var user2 = new AppUser { FullName = "Nigar Memmedova", UserName = "nigar", Email = "nigar@example.com", EmailConfirmed = true };
            await userManager.CreateAsync(user1, "Ali123!");
            await userManager.CreateAsync(user2, "Nigar123!");
            await userManager.AddToRoleAsync(user1, "User");
            await userManager.AddToRoleAsync(user2, "User");

            // Oyunlar (ApplicationCatalog)
            var applications = new List<ApplicationCatalog>
            {
                new ApplicationCatalog
                {
                    Name = "Cyberpunk 2077",
                    Description = "An open-world, action-adventure story set in Night City.",
                    ReleaseDate = new DateTime(2020, 12, 10),
                    Developer = "CD PROJEKT RED",
                    Publisher = "CD PROJEKT RED",
                    ApplicationType = "Game",
                    Genres = new List<Genre> { genres[0], genres[2] }, // Action, RPG
                    Tags = new List<Tag> { tags[1], tags[3] } // Singleplayer, Open World
                },
                new ApplicationCatalog
                {
                    Name = "The Witcher 3: Wild Hunt",
                    Description = "A story-driven, next-generation open world role-playing game.",
                    ReleaseDate = new DateTime(2015, 5, 19),
                    Developer = "CD PROJEKT RED",
                    Publisher = "CD PROJEKT RED",
                    ApplicationType = "Game",
                    Genres = new List<Genre> { genres[1], genres[2] }, // Adventure, RPG
                    Tags = new List<Tag> { tags[1], tags[3] } // Singleplayer, Open World
                },
                new ApplicationCatalog
                {
                    Name = "Counter-Strike: Global Offensive",
                    Description = "The classic competitive first-person shooter.",
                    ReleaseDate = new DateTime(2012, 8, 21),
                    Developer = "Valve",
                    Publisher = "Valve",
                    ApplicationType = "Game",
                    Genres = new List<Genre> { genres[0] }, // Action
                    Tags = new List<Tag> { tags[0], tags[4] } // Multiplayer, FPS
                }
            };
            await context.Applications.AddRangeAsync(applications);
            await context.SaveChangesAsync();

            // Media və Sistem Tələbləri
            var media = new List<Media>
            {
                new Media { ApplicationId = applications[0].Id, Url = "/uploads/cyberpunk1.jpg", MediaType = "Image", Order = 1 },
                new Media { ApplicationId = applications[1].Id, Url = "/uploads/witcher3.jpg", MediaType = "Image", Order = 1 },
                new Media { ApplicationId = applications[2].Id, Url = "/uploads/csgo.jpg", MediaType = "Image", Order = 1 }
            };
            await context.Media.AddRangeAsync(media);

            var systemRequirements = new List<SystemRequirements>
            {
                new SystemRequirements { ApplicationId = applications[0].Id, RequirementType = "Minimum", OS = "Windows 10", CPU = "Intel Core i5-3570K", GPU = "NVIDIA GeForce GTX 780", RAM = "8 GB", Storage = "70 GB" },
                new SystemRequirements { ApplicationId = applications[1].Id, RequirementType = "Minimum", OS = "Windows 7", CPU = "Intel CPU Core i5-2500K 3.3GHz", GPU = "Nvidia GPU GeForce GTX 660", RAM = "6 GB", Storage = "35 GB" }
            };
            await context.SystemRequirements.AddRangeAsync(systemRequirements);
            await context.SaveChangesAsync();

            // Qiymət nöqtələri və Regional Qiymətlər
            var pricePoints = new List<PricePoint>
            {
                new PricePoint { ApplicationId = applications[0].Id, Name = "Standard Edition", BasePrice = 59.99m },
                new PricePoint { ApplicationId = applications[1].Id, Name = "Standard Edition", BasePrice = 39.99m },
                new PricePoint { ApplicationId = applications[2].Id, Name = "Standard Edition", BasePrice = 14.99m }
            };
            await context.PricePoints.AddRangeAsync(pricePoints);
            await context.SaveChangesAsync();

            var regionalPrices = new List<RegionalPrice>
            {
                new RegionalPrice { PricePointId = pricePoints[0].Id, Currency = "USD", Amount = 59.99m },
                new RegionalPrice { PricePointId = pricePoints[0].Id, Currency = "AZN", Amount = 102.00m },
                new RegionalPrice { PricePointId = pricePoints[1].Id, Currency = "USD", Amount = 39.99m },
                new RegionalPrice { PricePointId = pricePoints[1].Id, Currency = "AZN", Amount = 68.00m },
            };
            await context.RegionalPrices.AddRangeAsync(regionalPrices);
            await context.SaveChangesAsync();


            // Rəylər (Reviews)
            var reviews = new List<Review>
            {
                new Review { UserId = user1.Id, ApplicationId = applications[0].Id, Title = "Amazing Game!", Content = "Night City is a breathtaking world to explore. The story is immersive and the characters are unforgettable. A must-play for any RPG fan.", IsRecommended = true, HelpfulCount = 15, FunnyCount = 2 },
                new Review { UserId = user2.Id, ApplicationId = applications[1].Id, Title = "A Masterpiece", Content = "The Witcher 3 is one of the best games I have ever played. The world is huge and full of interesting quests. The story is simply incredible.", IsRecommended = true, HelpfulCount = 25 }
            };
            await context.Reviews.AddRangeAsync(reviews);
            await context.SaveChangesAsync();

            // Kampaniyalar və Endirimlər
            var summerSale = new Campaign { Name = "Summer Sale 2024", Description = "Big discounts on great games!", StartDate = new DateTime(2024, 6, 20), EndDate = new DateTime(2024, 7, 10) };
            await context.Campaigns.AddAsync(summerSale);
            await context.SaveChangesAsync();

            var discounts = new List<Discount>
            {
                new Discount { ApplicationId = applications[0].Id, CampaignId = summerSale.Id, Percent = 50, StartDate = summerSale.StartDate, EndDate = summerSale.EndDate },
                new Discount { ApplicationId = applications[1].Id, Percent = 75, StartDate = DateTime.UtcNow.AddDays(-5), EndDate = DateTime.UtcNow.AddDays(5) }
            };
            await context.Discounts.AddRangeAsync(discounts);
            await context.SaveChangesAsync();
        }
    }
}
