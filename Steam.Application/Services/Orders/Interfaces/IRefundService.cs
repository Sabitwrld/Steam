using Steam.Application.DTOs.Orders.Refund;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IRefundService
    {
        Task<RefundReturnDto> RequestRefundAsync(RefundCreateDto dto);
        Task<RefundReturnDto> UpdateRefundStatusAsync(int id, string status); // For admin use
        Task<RefundReturnDto> GetRefundByIdAsync(int id);
        Task<PagedResponse<RefundListItemDto>> GetAllRefundsAsync(int pageNumber, int pageSize);
    }
}
