using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentReturnDto> CreatePaymentAsync(PaymentCreateDto dto);
        Task<PaymentReturnDto> UpdatePaymentAsync(int id, PaymentUpdateDto dto);
        Task<bool> DeletePaymentAsync(int id);
        Task<PaymentReturnDto> GetPaymentByIdAsync(int id);
        Task<PagedResponse<PaymentListItemDto>> GetAllPaymentsAsync(int pageNumber, int pageSize);
    }
}
