using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Refund;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Orders.Implementations
{
    public class RefundService : IRefundService
    {
        private readonly IRepository<Refund> _refundRepo;
        private readonly IRepository<Payment> _paymentRepo;
        private readonly IMapper _mapper;

        public RefundService(IRepository<Refund> refundRepo, IRepository<Payment> paymentRepo, IMapper mapper)
        {
            _refundRepo = refundRepo;
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<RefundReturnDto> RequestRefundAsync(RefundCreateDto dto)
        {
            var payment = await _paymentRepo.GetByIdAsync(dto.PaymentId);
            if (payment == null)
                throw new NotFoundException(nameof(Payment), dto.PaymentId);

            if (dto.Amount > payment.Amount)
                throw new System.Exception("Refund amount cannot be greater than the payment amount.");

            var refund = _mapper.Map<Refund>(dto);
            refund.Status = "Requested"; // Initial status

            await _refundRepo.CreateAsync(refund);
            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<RefundReturnDto> UpdateRefundStatusAsync(int id, string status)
        {
            var refund = await _refundRepo.GetByIdAsync(id);
            if (refund == null)
                throw new NotFoundException(nameof(Refund), id);

            // TODO: Add logic here to check for valid status transitions 
            // and maybe return money to the user if status is "Approved"
            refund.Status = status;
            refund.RefundDate = System.DateTime.UtcNow;

            await _refundRepo.UpdateAsync(refund);
            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<RefundReturnDto> GetRefundByIdAsync(int id)
        {
            var refund = await _refundRepo.GetByIdAsync(id);
            if (refund == null)
                throw new NotFoundException(nameof(Refund), id);

            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<PagedResponse<RefundListItemDto>> GetAllRefundsAsync(int pageNumber, int pageSize)
        {
            var query = _refundRepo.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<RefundListItemDto>
            {
                Data = _mapper.Map<List<RefundListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
