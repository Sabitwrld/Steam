using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Discount;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountReturnDto> CreateDiscountAsync(DiscountCreateDto dto);
        Task<DiscountReturnDto> UpdateDiscountAsync(int id, DiscountUpdateDto dto);
        Task<bool> DeleteDiscountAsync(int id);
        Task<DiscountReturnDto> GetDiscountByIdAsync(int id);
        Task<PagedResponse<DiscountListItemDto>> GetAllDiscountsAsync(int pageNumber, int pageSize);
    }
}
