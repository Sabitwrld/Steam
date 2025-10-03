using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;
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
            await _repository.CreateAsync(entity);
            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<PricePointReturnDto> UpdatePricePointAsync(int id, PricePointUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(PricePoint), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PricePointReturnDto>(entity);
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
            var entity = await _repository.GetEntityAsync(
                predicate: p => p.Id == id,
                includes: new Func<IQueryable<PricePoint>, IQueryable<PricePoint>>[] { q => q.Include(p => p.RegionalPrices) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(PricePoint), id);

            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<PagedResponse<PricePointListItemDto>> GetAllPricePointsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<PricePointListItemDto>
            {
                Data = _mapper.Map<List<PricePointListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
