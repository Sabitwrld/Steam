using Steam.Application.DTOs.Orders.Refund;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IRefundService
    {
        Task<RefundReturnDto> CreateRefundAsync(RefundCreateDto dto);
        Task<RefundReturnDto> UpdateRefundAsync(int id, RefundUpdateDto dto);
        Task<bool> DeleteRefundAsync(int id);
        Task<RefundReturnDto> GetRefundByIdAsync(int id);
        Task<PagedResponse<RefundListItemDto>> GetAllRefundsAsync(int pageNumber, int pageSize);
    }
}
