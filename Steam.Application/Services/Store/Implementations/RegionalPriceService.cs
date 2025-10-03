using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;
using Steam.Application.Exceptions;
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
            await _repository.CreateAsync(entity);
            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<RegionalPriceReturnDto> UpdateRegionalPriceAsync(int id, RegionalPriceUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(RegionalPrice), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<RegionalPriceReturnDto>(entity);
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
                throw new NotFoundException(nameof(RegionalPrice), id);

            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<PagedResponse<RegionalPriceListItemDto>> GetAllRegionalPricesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<RegionalPriceListItemDto>
            {
                Data = _mapper.Map<List<RegionalPriceListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
