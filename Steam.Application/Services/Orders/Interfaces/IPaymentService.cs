using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
