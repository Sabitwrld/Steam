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
            // Achievement Mappings
            CreateMap<AchievementCreateDto, Achievement>();
            CreateMap<AchievementUpdateDto, Achievement>();
            CreateMap<Achievement, AchievementReturnDto>();
            CreateMap<Achievement, AchievementListItemDto>();

            // UserAchievement Mappings
            CreateMap<UserAchievementCreateDto, UserAchievement>();
            CreateMap<UserAchievement, UserAchievementReturnDto>();
            CreateMap<UserAchievement, UserAchievementListItemDto>()
                .ForMember(dest => dest.AchievementName, opt => opt.MapFrom(src => src.Achievement.Name))
                .ForMember(dest => dest.AchievementIconUrl, opt => opt.MapFrom(src => src.Achievement.IconUrl));

            // Badge Mappings
            CreateMap<BadgeCreateDto, Badge>();
            CreateMap<BadgeUpdateDto, Badge>();
            CreateMap<Badge, BadgeReturnDto>();
            CreateMap<Badge, BadgeListItemDto>();

            // Leaderboard Mappings
            CreateMap<LeaderboardCreateDto, Leaderboard>();
            CreateMap<LeaderboardUpdateDto, Leaderboard>();
            CreateMap<Leaderboard, LeaderboardReturnDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<Leaderboard, LeaderboardListItemDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            // CraftingRecipe Mappings
            CreateMap<CraftingRecipeCreateDto, CraftingRecipe>();
            CreateMap<CraftingRecipeUpdateDto, CraftingRecipe>();
            CreateMap<CraftingRecipe, CraftingRecipeReturnDto>()
                .ForMember(dest => dest.Requirements, opt => opt.MapFrom(src => src.Requirements.Select(r => r.RequiredBadge)));
            CreateMap<CraftingRecipe, CraftingRecipeListItemDto>()
                .ForMember(dest => dest.ResultBadgeName, opt => opt.MapFrom(src => src.ResultBadge.Name));
        }
    }
}
