using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Library.Interfaces;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Constants;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Orders.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IUserLibraryService _userLibraryService;
        private readonly ILicenseService _licenseService;
        private readonly IMapper _mapper;

        public PaymentService(
            IUnitOfWork unitOfWork, // Dəyişdirildi
            IUserLibraryService userLibraryService,
            ILicenseService licenseService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userLibraryService = userLibraryService;
            _licenseService = licenseService;
            _mapper = mapper;
        }

        public async Task<PaymentReturnDto> CreatePaymentAsync(PaymentCreateDto dto)
        {
            var order = await _unitOfWork.OrderRepository.GetEntityAsync(
                predicate: o => o.Id == dto.OrderId,
                includes: new[] { (Func<IQueryable<Order>, IQueryable<Order>>)(q => q.Include(o => o.Items)) }
            );
            if (order == null)
                throw new NotFoundException(nameof(Order), dto.OrderId);

            if (order.Status != OrderStatus.Pending) // DƏYİŞDİRİLDİ
                throw new Exception("This order cannot be paid for.");

            var payment = _mapper.Map<Payment>(dto);
            payment.Amount = order.TotalPrice;
            payment.Status = "Paid"; // Bu status ödəniş sistemindən asılı olaraq dəyişə bilər, hələlik qalsın
            payment.PaymentDate = DateTime.UtcNow;
            payment.TransactionId = Guid.NewGuid().ToString();

            await _unitOfWork.PaymentRepository.CreateAsync(payment);

            order.Status = OrderStatus.Completed; // DƏYİŞDİRİLDİ
            _unitOfWork.OrderRepository.Update(order);

            var userLibrary = await _userLibraryService.GetUserLibraryByUserIdAsync(order.UserId);
            foreach (var item in order.Items)
            {
                var licenseDto = new DTOs.Library.License.LicenseCreateDto
                {
                    ApplicationId = item.ApplicationId,
                    LicenseType = LicenseTypes.Lifetime // DƏYİŞDİRİLDİ
                };
                await _licenseService.AddLicenseAsync(userLibrary.Id, licenseDto);
            }

            await _unitOfWork.CommitAsync();

            return _mapper.Map<PaymentReturnDto>(payment);
        }

        public async Task<PaymentReturnDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id); // Dəyişdirildi
            if (payment == null)
                throw new NotFoundException(nameof(Payment), id);
            return _mapper.Map<PaymentReturnDto>(payment);
        }

        public async Task<PaymentReturnDto> UpdatePaymentAsync(int id, PaymentUpdateDto dto)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id); // Dəyişdirildi
            if (payment == null)
                throw new NotFoundException(nameof(Payment), id);

            _mapper.Map(dto, payment);
            _unitOfWork.PaymentRepository.Update(payment); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi
            return _mapper.Map<PaymentReturnDto>(payment);
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id); // Dəyişdirildi
            if (payment == null)
                return false;

            _unitOfWork.PaymentRepository.Delete(payment); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi
            return true;
        }

        public async Task<PagedResponse<PaymentListItemDto>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.PaymentRepository.GetQuery(asNoTracking: true); // Dəyişdirildi
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
