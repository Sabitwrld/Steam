using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Wishlist;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WishlistReturnDto> CreateWishlistAsync(WishlistCreateDto dto)
        {
            var existing = await _unitOfWork.WishlistRepository.IsExistsAsync(w => w.UserId == dto.UserId && w.ApplicationId == dto.ApplicationId);
            if (existing)
            {
                throw new Exception("This item is already in the wishlist.");
            }

            var entity = _mapper.Map<Wishlist>(dto);
            await _unitOfWork.WishlistRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<WishlistReturnDto>(entity);
        }

        public async Task<bool> DeleteWishlistAsync(int id)
        {
            var entity = await _unitOfWork.WishlistRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.WishlistRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteWishlistByAppIdAsync(string userId, int applicationId)
        {
            var entity = await _unitOfWork.WishlistRepository.GetEntityAsync(w => w.UserId == userId && w.ApplicationId == applicationId);
            if (entity == null)
                return false;

            _unitOfWork.WishlistRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<WishlistReturnDto> GetWishlistByIdAsync(int id)
        {
            var entity = await _unitOfWork.WishlistRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Wishlist), id);

            return _mapper.Map<WishlistReturnDto>(entity);
        }

        public async Task<PagedResponse<WishlistListItemDto>> GetAllWishlistsAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.WishlistRepository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<WishlistListItemDto>
            {
                Data = _mapper.Map<List<WishlistListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResponse<WishlistListItemDto>> GetWishlistByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.WishlistRepository.GetQuery(w => w.UserId == userId, asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<WishlistListItemDto>
            {
                Data = _mapper.Map<List<WishlistListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
