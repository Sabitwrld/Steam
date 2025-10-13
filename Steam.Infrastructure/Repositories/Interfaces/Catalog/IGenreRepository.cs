using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<(IEnumerable<Genre> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
