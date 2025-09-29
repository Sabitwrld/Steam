using Steam.Application.DTOs.Achievements.Achievements;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
