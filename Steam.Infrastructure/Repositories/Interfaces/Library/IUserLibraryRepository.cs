using Steam.Domain.Entities.Library;

namespace Steam.Infrastructure.Repositories.Interfaces.Library
{
    public interface IUserLibraryRepository : IRepository<UserLibrary>
    {
        Task<(IEnumerable<UserLibrary> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
