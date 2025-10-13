using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Repositories.Interfaces.Achievements
{
    public interface IBadgeRepository : IRepository<Badge>
    {
        Task<(IEnumerable<Badge> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
