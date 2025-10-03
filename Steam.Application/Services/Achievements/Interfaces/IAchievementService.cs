using Steam.Application.DTOs.Achievements.Achievements;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface IAchievementService
    {
        Task<AchievementReturnDto> CreateAchievementAsync(AchievementCreateDto dto);
        Task<AchievementReturnDto> UpdateAchievementAsync(int id, AchievementUpdateDto dto);
        Task<bool> DeleteAchievementAsync(int id);
        Task<AchievementReturnDto> GetAchievementByIdAsync(int id);
        Task<PagedResponse<AchievementListItemDto>> GetAllAchievementsAsync(int pageNumber, int pageSize);
    }
}
