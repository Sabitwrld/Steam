using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<(IEnumerable<Tag> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
