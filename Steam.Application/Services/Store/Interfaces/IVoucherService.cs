using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Voucher;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherReturnDto> CreateVoucherAsync(VoucherCreateDto dto);
        Task<VoucherReturnDto> GetVoucherByIdAsync(int id);
        Task<PagedResponse<VoucherListItemDto>> GetAllVouchersAsync(int pageNumber, int pageSize);
        Task<VoucherReturnDto> RedeemVoucherAsync(string code, string userId); // FIXED
    }
}
