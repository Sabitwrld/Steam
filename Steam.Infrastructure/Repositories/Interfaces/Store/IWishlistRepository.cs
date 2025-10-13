using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<(IEnumerable<Wishlist> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<Wishlist> Items, int TotalCount)> GetByUserIdPagedAsync(string userId, int pageNumber, int pageSize);
    }
}
