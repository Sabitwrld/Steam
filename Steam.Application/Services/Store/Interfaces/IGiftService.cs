using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Gift;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IGiftService
    {
        Task<GiftReturnDto> SendGiftAsync(GiftCreateDto dto);
        Task<bool> RedeemGiftAsync(int giftId, int receiverId);
        Task<GiftReturnDto> GetGiftByIdAsync(int id);
        Task<PagedResponse<GiftListItemDto>> GetGiftsForUserAsync(int userId, int pageNumber, int pageSize);
    }
}
