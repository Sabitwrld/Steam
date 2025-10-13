using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IGiftRepository : IRepository<Gift>
    {
        Task<(IEnumerable<Gift> Items, int TotalCount)> GetByUserIdPagedAsync(string userId, int pageNumber, int pageSize);

    }
}
