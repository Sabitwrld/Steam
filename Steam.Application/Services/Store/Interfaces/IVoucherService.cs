using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherReturnDto> CreateVoucherAsync(VoucherCreateDto dto);
        Task<VoucherReturnDto> RedeemVoucherAsync(string code, int userId);
        Task<VoucherReturnDto> GetVoucherByIdAsync(int id);
        Task<PagedResponse<VoucherListItemDto>> GetAllVouchersAsync(int pageNumber, int pageSize);
    }
}
