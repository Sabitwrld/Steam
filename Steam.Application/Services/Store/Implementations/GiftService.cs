using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Gift;
using Steam.Application.Exceptions;
using Steam.Application.Services.Library.Interfaces;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class GiftService : IGiftService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IUserLibraryService _userLibraryService;
        private readonly ILicenseService _licenseService;
        private readonly IMapper _mapper;

        public GiftService(IUnitOfWork unitOfWork, IMapper mapper, IUserLibraryService userLibraryService, ILicenseService licenseService) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userLibraryService = userLibraryService;
            _licenseService = licenseService;
        }

        public async Task<GiftReturnDto> SendGiftAsync(GiftCreateDto dto)
        {
            var entity = _mapper.Map<Gift>(dto);
            entity.SentAt = DateTime.UtcNow;
            await _unitOfWork.GiftRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<bool> RedeemGiftAsync(int giftId, string receiverId)
        {
            var gift = await _unitOfWork.GiftRepository.GetEntityAsync(g => g.Id == giftId && g.ReceiverId == receiverId);
            if (gift == null)
                throw new NotFoundException("Gift not found or does not belong to this user.");

            if (gift.IsRedeemed)
                throw new Exception("This gift has already been redeemed.");

            var userLibrary = await _userLibraryService.GetUserLibraryByUserIdAsync(receiverId);
            var licenseDto = new DTOs.Library.License.LicenseCreateDto
            {
                ApplicationId = gift.ApplicationId,
                LicenseType = "Lifetime"
            };
            await _licenseService.AddLicenseAsync(userLibrary.Id, licenseDto);

            gift.IsRedeemed = true;
            _unitOfWork.GiftRepository.Update(gift);

            await _unitOfWork.CommitAsync(); // Lisenziya və hədiyyə statusu birgə yadda saxlanılır

            return true;
        }

        public async Task<GiftReturnDto> GetGiftByIdAsync(int id)
        {
            var entity = await _unitOfWork.GiftRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Gift), id);

            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<PagedResponse<GiftListItemDto>> GetGiftsForUserAsync(string userId, int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.GiftRepository.GetByUserIdPagedAsync(userId, pageNumber, pageSize);

            return new PagedResponse<GiftListItemDto>
            {
                Data = _mapper.Map<List<GiftListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
