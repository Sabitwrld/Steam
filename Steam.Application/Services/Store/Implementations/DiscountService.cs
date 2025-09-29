using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Discount;
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
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> _repository;
        private readonly IMapper _mapper;

        public DiscountService(IRepository<Discount> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DiscountReturnDto> CreateDiscountAsync(DiscountCreateDto dto)
        {
            var entity = _mapper.Map<Discount>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<DiscountReturnDto>(created);
        }

        public async Task<DiscountReturnDto> UpdateDiscountAsync(int id, DiscountUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Discount with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<DiscountReturnDto>(updated);
        }

        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<DiscountReturnDto> GetDiscountByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Discount with Id {id} not found.");

            return _mapper.Map<DiscountReturnDto>(entity);
        }

        public async Task<PagedResponse<DiscountListItemDto>> GetAllDiscountsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<DiscountListItemDto>>(items);

            return new PagedResponse<DiscountListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
