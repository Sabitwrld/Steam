using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Voucher;
using Steam.Application.Exceptions;
using Steam.Application.Services.Library.Interfaces;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IUserLibraryService _userLibraryService;
        private readonly ILicenseService _licenseService;
        private readonly IMapper _mapper;

        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper, IUserLibraryService userLibraryService, ILicenseService licenseService) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userLibraryService = userLibraryService;
            _licenseService = licenseService;
        }

        public async Task<VoucherReturnDto> CreateVoucherAsync(VoucherCreateDto dto)
        {
            var entity = _mapper.Map<Voucher>(dto);
            await _unitOfWork.VoucherRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<VoucherReturnDto>(entity);
        }

        public async Task<VoucherReturnDto> RedeemVoucherAsync(string code, string userId)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetEntityAsync(v => v.Code == code);

            if (voucher == null)
                throw new NotFoundException("Voucher with this code does not exist.");

            if (voucher.IsUsed)
                throw new Exception("This voucher has already been used.");

            if (voucher.ExpirationDate < DateTime.UtcNow)
                throw new Exception("This voucher has expired.");

            var userLibrary = await _userLibraryService.GetUserLibraryByUserIdAsync(userId);
            var licenseDto = new DTOs.Library.License.LicenseCreateDto
            {
                ApplicationId = voucher.ApplicationId,
                LicenseType = "Lifetime"
            };
            await _licenseService.AddLicenseAsync(userLibrary.Id, licenseDto);

            voucher.IsUsed = true;
            _unitOfWork.VoucherRepository.Update(voucher);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<VoucherReturnDto>(voucher);
        }

        public async Task<VoucherReturnDto> GetVoucherByIdAsync(int id)
        {
            var entity = await _unitOfWork.VoucherRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Voucher), id);

            return _mapper.Map<VoucherReturnDto>(entity);
        }

        public async Task<PagedResponse<VoucherListItemDto>> GetAllVouchersAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.VoucherRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<VoucherListItemDto>
            {
                Data = _mapper.Map<List<VoucherListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
