using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<(IEnumerable<Campaign> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
