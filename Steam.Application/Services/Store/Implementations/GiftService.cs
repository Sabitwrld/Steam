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
        private readonly IRepository<Gift> _repository;
        private readonly IUserLibraryService _userLibraryService; // ADD THIS
        private readonly ILicenseService _licenseService;         // ADD THIS
        private readonly IMapper _mapper;

        public GiftService(IRepository<Gift> repository, IMapper mapper, IUserLibraryService userLibraryService, ILicenseService licenseService)
        {
            _repository = repository;
            _mapper = mapper;
            _userLibraryService = userLibraryService;
            _licenseService = licenseService;
        }

        public async Task<GiftReturnDto> SendGiftAsync(GiftCreateDto dto)
        {
            var entity = _mapper.Map<Gift>(dto);
            entity.SentAt = System.DateTime.UtcNow;
            await _repository.CreateAsync(entity);
            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<bool> RedeemGiftAsync(int giftId, string receiverId) // UserId is now string
        {
            var gift = await _repository.GetEntityAsync(g => g.Id == giftId && g.ReceiverId == receiverId);
            if (gift == null)
                throw new NotFoundException("Gift not found or does not belong to this user.");

            if (gift.IsRedeemed)
                throw new System.Exception("This gift has already been redeemed.");

            // --- INTEGRATION LOGIC: Add the gifted game to the user's library ---
            var userLibrary = await _userLibraryService.GetUserLibraryByUserIdAsync(receiverId);
            var licenseDto = new DTOs.Library.License.LicenseCreateDto
            {
                ApplicationId = gift.ApplicationId,
                LicenseType = "Lifetime"
            };
            await _licenseService.AddLicenseAsync(userLibrary.Id, licenseDto);
            // --- END INTEGRATION ---

            gift.IsRedeemed = true;
            await _repository.UpdateAsync(gift);

            return true;
        }

        public async Task<GiftReturnDto> GetGiftByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Gift), id);

            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<PagedResponse<GiftListItemDto>> GetGiftsForUserAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(g => g.ReceiverId == userId, asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(g => g.SentAt)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

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
