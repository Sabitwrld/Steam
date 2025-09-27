using AutoMapper;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class SystemRequirementsService : ISystemRequirementsService
    {
        private readonly IRepository<SystemRequirements> _repository;
        private readonly IMapper _mapper;

        public SystemRequirementsService(IRepository<SystemRequirements> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SystemRequirementsReturnDto> CreateSystemRequirementsAsync(SystemRequirementsCreateDto dto)
        {
            var entity = _mapper.Map<SystemRequirements>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<SystemRequirementsReturnDto>(created);
        }

        public async Task<SystemRequirementsReturnDto> UpdateSystemRequirementsAsync(int id, SystemRequirementsUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"SystemRequirements with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<SystemRequirementsReturnDto>(updated);
        }

        public async Task<bool> DeleteSystemRequirementsAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<SystemRequirementsReturnDto> GetSystemRequirementsByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"SystemRequirements with Id {id} not found.");

            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<PagedResponse<SystemRequirementsListItemDto>> GetAllSystemRequirementsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<SystemRequirementsListItemDto>>(items);

            return new PagedResponse<SystemRequirementsListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };

        }
    }
}
