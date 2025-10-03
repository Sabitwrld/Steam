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
    }
}
