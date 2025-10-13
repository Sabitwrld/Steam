using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Repositories.Interfaces.Achievements
{
    public interface IAchievementRepository : IRepository<Achievement>
    {
        Task<(IEnumerable<Achievement> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
