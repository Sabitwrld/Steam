using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Library;

namespace Steam.Infrastructure.Repositories.Implementations.Library
{
    public class UserLibraryRepository : Repository<UserLibrary>, IUserLibraryRepository
    {
        public UserLibraryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<UserLibrary> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Include(ul => ul.Licenses) // Servisdəki `Include` məntiqi bura daşındı
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
