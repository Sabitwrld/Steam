using AutoMapper;
using Steam.Application.DTOs.Achievements.Achievements;
using Steam.Application.DTOs.Achievements.Badge;
using Steam.Application.DTOs.Achievements.CraftingRecipe;
using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Domain.Entities.Achievements;

namespace Steam.Application.Profiles
{
    public class AchievementsMappingProfile : Profile
    {
        public AchievementsMappingProfile()
        {
            // Achievement
            CreateMap<AchievementCreateDto, Achievement>();
            CreateMap<AchievementUpdateDto, Achievement>();
            CreateMap<Achievement, AchievementReturnDto>();
            CreateMap<Achievement, AchievementListItemDto>();

            // UserAchievement
            CreateMap<UserAchievementCreateDto, UserAchievement>();
            CreateMap<UserAchievementUpdateDto, UserAchievement>();
            CreateMap<UserAchievement, UserAchievementReturnDto>();
            CreateMap<UserAchievement, UserAchievementListItemDto>();

            // Leaderboard
            CreateMap<LeaderboardCreateDto, Leaderboard>();
            CreateMap<LeaderboardUpdateDto, Leaderboard>();
            CreateMap<Leaderboard, LeaderboardReturnDto>();
            CreateMap<Leaderboard, LeaderboardListItemDto>();

            // Badge
            CreateMap<BadgeCreateDto, Badge>();
            CreateMap<BadgeUpdateDto, Badge>();
            CreateMap<Badge, BadgeReturnDto>();
            CreateMap<Badge, BadgeListItemDto>();

            // CraftingRecipe
            CreateMap<CraftingRecipeCreateDto, CraftingRecipe>();
            CreateMap<CraftingRecipeUpdateDto, CraftingRecipe>();
            CreateMap<CraftingRecipe, CraftingRecipeReturnDto>();
            CreateMap<CraftingRecipe, CraftingRecipeListItemDto>();
        }
    }
}
