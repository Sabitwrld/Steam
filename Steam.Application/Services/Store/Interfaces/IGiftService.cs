using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Gift;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IGiftService
    {
        Task<GiftReturnDto> SendGiftAsync(GiftCreateDto dto);
        Task<bool> RedeemGiftAsync(int giftId, string receiverId); // FIXED
        Task<GiftReturnDto> GetGiftByIdAsync(int id);
        Task<PagedResponse<GiftListItemDto>> GetGiftsForUserAsync(string userId, int pageNumber, int pageSize); // FIXED
    }
}
