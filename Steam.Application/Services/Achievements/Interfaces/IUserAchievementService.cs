using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface IUserAchievementService
    {
        Task<UserAchievementReturnDto> CreateUserAchievementAsync(UserAchievementCreateDto dto);
        Task<UserAchievementReturnDto> UpdateUserAchievementAsync(int id, UserAchievementUpdateDto dto);
        Task<bool> DeleteUserAchievementAsync(int id);
        Task<UserAchievementReturnDto> GetUserAchievementByIdAsync(int id);
        Task<PagedResponse<UserAchievementListItemDto>> GetAllUserAchievementsAsync(int pageNumber, int pageSize);
    }
}
