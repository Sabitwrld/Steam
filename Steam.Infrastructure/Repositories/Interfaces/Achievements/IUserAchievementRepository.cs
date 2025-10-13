using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Repositories.Interfaces.Achievements
{
    public interface IUserAchievementRepository : IRepository<UserAchievement>
    {
        Task<(IEnumerable<UserAchievement> Items, int TotalCount)> GetByUserIdPagedAsync(string userId, int pageNumber, int pageSize);

    }
}
