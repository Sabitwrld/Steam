using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface ILeaderboardService
    {
        Task<LeaderboardReturnDto> AddOrUpdateScoreAsync(LeaderboardCreateDto dto);
        Task<bool> DeleteScoreAsync(int id);
        Task<LeaderboardReturnDto> GetScoreByIdAsync(int id);
        Task<PagedResponse<LeaderboardListItemDto>> GetLeaderboardForApplicationAsync(int applicationId, int pageNumber, int pageSize);
    }
}
