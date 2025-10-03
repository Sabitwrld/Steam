using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Orders.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _paymentRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> paymentRepo, IRepository<Order> orderRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<PaymentReturnDto> CreatePaymentAsync(PaymentCreateDto dto)
        {
            var order = await _orderRepo.GetByIdAsync(dto.OrderId);
            if (order == null)
                throw new NotFoundException(nameof(Order), dto.OrderId);

            var payment = _mapper.Map<Payment>(dto);
            payment.Amount = order.TotalPrice;
            // Simulate a successful payment
            payment.Status = "Paid";
            payment.PaymentDate = System.DateTime.UtcNow;
            payment.TransactionId = System.Guid.NewGuid().ToString();
            await _paymentRepo.CreateAsync(payment);

            // Update order status
            order.Status = "Completed";
            await _orderRepo.UpdateAsync(order);

            return _mapper.Map<PaymentReturnDto>(payment);
        }

        public async Task<PaymentReturnDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException(nameof(Payment), id);
            return _mapper.Map<PaymentReturnDto>(payment);
        }

        // --- MISSING METHODS ADDED BELOW ---

        public async Task<PaymentReturnDto> UpdatePaymentAsync(int id, PaymentUpdateDto dto)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException(nameof(Payment), id);

            _mapper.Map(dto, payment);
            await _paymentRepo.UpdateAsync(payment);
            return _mapper.Map<PaymentReturnDto>(payment);
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepo.GetByIdAsync(id);
            if (payment == null)
                return false;

            return await _paymentRepo.DeleteAsync(payment);
        }

        public async Task<PagedResponse<PaymentListItemDto>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            var query = _paymentRepo.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<PaymentListItemDto>
            {
                Data = _mapper.Map<List<PaymentListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
