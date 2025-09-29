using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;
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
    public class PricePointService : IPricePointService
    {
        private readonly IRepository<PricePoint> _repository;
        private readonly IMapper _mapper;

        public PricePointService(IRepository<PricePoint> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PricePointReturnDto> CreatePricePointAsync(PricePointCreateDto dto)
        {
            var entity = _mapper.Map<PricePoint>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<PricePointReturnDto>(created);
        }

        public async Task<PricePointReturnDto> UpdatePricePointAsync(int id, PricePointUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"PricePoint with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<PricePointReturnDto>(updated);
        }

        public async Task<bool> DeletePricePointAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<PricePointReturnDto> GetPricePointByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"PricePoint with Id {id} not found.");

            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<PagedResponse<PricePointListItemDto>> GetAllPricePointsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<PricePointListItemDto>>(items);

            return new PagedResponse<PricePointListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
