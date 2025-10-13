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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public RefundService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RefundReturnDto> RequestRefundAsync(RefundCreateDto dto)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(dto.PaymentId); // Dəyişdirildi
            if (payment == null)
                throw new NotFoundException(nameof(Payment), dto.PaymentId);

            if (dto.Amount > payment.Amount)
                throw new Exception("Refund amount cannot be greater than the payment amount.");

            var refund = _mapper.Map<Refund>(dto);
            refund.Status = "Requested";

            await _unitOfWork.RefundRepository.CreateAsync(refund); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi

            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<RefundReturnDto> UpdateRefundStatusAsync(int id, string status)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(id); // Dəyişdirildi
            if (refund == null)
                throw new NotFoundException(nameof(Refund), id);

            refund.Status = status;
            refund.RefundDate = DateTime.UtcNow;

            _unitOfWork.RefundRepository.Update(refund); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi

            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<RefundReturnDto> GetRefundByIdAsync(int id)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(id); // Dəyişdirildi
            if (refund == null)
                throw new NotFoundException(nameof(Refund), id);

            return _mapper.Map<RefundReturnDto>(refund);
        }

        public async Task<PagedResponse<RefundListItemDto>> GetAllRefundsAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.RefundRepository.GetQuery(asNoTracking: true); // Dəyişdirildi
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
