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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserLibraryService _userLibraryService;
        private readonly ILicenseService _licenseService;
        private readonly IMapper _mapper;

        public PaymentService(
            IUnitOfWork unitOfWork, 
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
            // Addım 1: Tranzaksiyanı başlat
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Addım 2: Sifarişi əldə et və yoxla
                var order = await _unitOfWork.OrderRepository.GetEntityAsync(
                    predicate: o => o.Id == dto.OrderId,
                    includes: new[] { (Func<IQueryable<Order>, IQueryable<Order>>)(q => q.Include(o => o.Items)) }
                );

                if (order == null)
                    throw new NotFoundException(nameof(Order), dto.OrderId);

                if (order.Status != OrderStatus.Pending)
                    throw new InvalidOperationException("This order cannot be paid for as its status is not 'Pending'.");

                // Addım 3: Ödənişi yarat
                var payment = _mapper.Map<Payment>(dto);
                payment.Amount = order.TotalPrice;
                payment.Status = "Paid"; // Real-world-da bu, ödəniş sistemindən gələn cavaba görə təyin edilməlidir
                payment.PaymentDate = DateTime.UtcNow;
                payment.TransactionId = Guid.NewGuid().ToString(); // Bu da ödəniş sistemindən gəlməlidir

                await _unitOfWork.PaymentRepository.CreateAsync(payment);

                // Addım 4: Sifariş statusunu yenilə
                order.Status = OrderStatus.Completed;
                _unitOfWork.OrderRepository.Update(order);

                // Addım 5: Lisenziyaları istifadəçinin kitabxanasına əlavə et
                var userLibrary = await _userLibraryService.GetUserLibraryByUserIdAsync(order.UserId);
                foreach (var item in order.Items)
                {
                    var licenseDto = new DTOs.Library.License.LicenseCreateDto
                    {
                        ApplicationId = item.ApplicationId,
                        LicenseType = LicenseTypes.Lifetime // <-- Gördüyünüz kimi, burada həmişə Lifetime təyin edilir.
                    };
                    await _licenseService.AddLicenseAsync(userLibrary.Id, licenseDto);
                }

                // Addım 6: Bütün dəyişiklikləri verilənlər bazasına göndər
                await _unitOfWork.CommitAsync();

                // Addım 7: Hər şey uğurludursa, tranzaksiyanı təsdiqlə (Commit)
                await transaction.CommitAsync();

                return _mapper.Map<PaymentReturnDto>(payment);
            }
            catch (Exception)
            {
                // Addım 8: Hər hansı bir xəta baş verərsə, tranzaksiyanı ləğv et (Rollback)
                await transaction.RollbackAsync();
                throw; // Xətanı yuxarıdakı qatlara (Middleware) ötür ki, istifadəçiyə uyğun cavab verilsin.
            }
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
            var (items, totalCount) = await _unitOfWork.PaymentRepository.GetAllPagedAsync(pageNumber, pageSize);

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
