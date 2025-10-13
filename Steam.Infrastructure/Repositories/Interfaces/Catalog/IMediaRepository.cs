using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IMediaRepository : IRepository<Media>
    {
        Task<List<Media>> GetByApplicationIdAsync(int applicationId);
        Task<(IEnumerable<Media> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
