using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Repositories.Interfaces.Achievements
{
    public interface ILeaderboardRepository : IRepository<Leaderboard>
    {
        Task<(IEnumerable<Leaderboard> Items, int TotalCount)> GetByApplicationIdPagedAsync(int applicationId, int pageNumber, int pageSize);

    }
}
