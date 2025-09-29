using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Wishlist;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Store.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<Wishlist> _repository;
        private readonly IMapper _mapper;

        public WishlistService(IRepository<Wishlist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WishlistReturnDto> CreateWishlistAsync(WishlistCreateDto dto)
        {
            var entity = _mapper.Map<Wishlist>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<WishlistReturnDto>(created);
        }

        public async Task<WishlistReturnDto> UpdateWishlistAsync(int id, WishlistUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Wishlist with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<WishlistReturnDto>(updated);
        }

        public async Task<bool> DeleteWishlistAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<WishlistReturnDto> GetWishlistByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Wishlist with Id {id} not found.");

            return _mapper.Map<WishlistReturnDto>(entity);
        }

        public async Task<PagedResponse<WishlistListItemDto>> GetAllWishlistsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<WishlistListItemDto>>(items);

            return new PagedResponse<WishlistListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
