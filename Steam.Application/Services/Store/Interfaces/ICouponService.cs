using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Coupon;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface ICouponService
    {
        Task<CouponReturnDto> CreateCouponAsync(CouponCreateDto dto);
        Task<CouponReturnDto> UpdateCouponAsync(int id, CouponUpdateDto dto);
        Task<bool> DeleteCouponAsync(int id);
        Task<CouponReturnDto> GetCouponByIdAsync(int id);
        Task<PagedResponse<CouponListItemDto>> GetAllCouponsAsync(int pageNumber, int pageSize);
    }
}
