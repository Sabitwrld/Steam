using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface IUserAchievementService
    {
        Task<UserAchievementReturnDto> UnlockAchievementAsync(UserAchievementCreateDto dto);
        Task<bool> DeleteUserAchievementAsync(int id);
        Task<UserAchievementReturnDto> GetUserAchievementByIdAsync(int id);
        Task<PagedResponse<UserAchievementListItemDto>> GetAchievementsForUserAsync(string userId, int pageNumber, int pageSize);
    }
}
