using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface ILeaderboardService
    {
        Task<LeaderboardReturnDto> CreateLeaderboardAsync(LeaderboardCreateDto dto);
        Task<LeaderboardReturnDto> UpdateLeaderboardAsync(int id, LeaderboardUpdateDto dto);
        Task<bool> DeleteLeaderboardAsync(int id);
        Task<LeaderboardReturnDto> GetLeaderboardByIdAsync(int id);
        Task<PagedResponse<LeaderboardListItemDto>> GetAllLeaderboardsAsync(int pageNumber, int pageSize);
    }
}
