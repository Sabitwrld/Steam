using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Wishlist;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IWishlistService
    {
        Task<WishlistReturnDto> CreateWishlistAsync(WishlistCreateDto dto);
        Task<bool> DeleteWishlistAsync(int id);
        Task<WishlistReturnDto> GetWishlistByIdAsync(int id);
        Task<PagedResponse<WishlistListItemDto>> GetAllWishlistsAsync(int pageNumber, int pageSize);
    }
}
