using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Pagination;

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
