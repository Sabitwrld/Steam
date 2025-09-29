using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;
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
    public class RegionalPriceService : IRegionalPriceService
    {
        private readonly IRepository<RegionalPrice> _repository;
        private readonly IMapper _mapper;

        public RegionalPriceService(IRepository<RegionalPrice> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RegionalPriceReturnDto> CreateRegionalPriceAsync(RegionalPriceCreateDto dto)
        {
            var entity = _mapper.Map<RegionalPrice>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<RegionalPriceReturnDto>(created);
        }

        public async Task<RegionalPriceReturnDto> UpdateRegionalPriceAsync(int id, RegionalPriceUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"RegionalPrice with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<RegionalPriceReturnDto>(updated);
        }

        public async Task<bool> DeleteRegionalPriceAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<RegionalPriceReturnDto> GetRegionalPriceByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"RegionalPrice with Id {id} not found.");

            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<PagedResponse<RegionalPriceListItemDto>> GetAllRegionalPricesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<RegionalPriceListItemDto>>(items);

            return new PagedResponse<RegionalPriceListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
