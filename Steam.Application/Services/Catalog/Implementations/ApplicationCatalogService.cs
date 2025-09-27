using AutoMapper;
using Steam.Application.DTOs.Catalog.Application;
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
    public class ApplicationCatalogService : IApplicationCatalogService
    {
        private readonly IRepository<ApplicationCatalog> _repository;
        private readonly IMapper _mapper;

        public ApplicationCatalogService(IRepository<ApplicationCatalog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApplicationCatalogReturnDto> CreateApplicationCatalogAsync(ApplicationCatalogCreateDto dto)
        {
            var entity = _mapper.Map<ApplicationCatalog>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(created);
        }

        public async Task<ApplicationCatalogReturnDto> UpdateApplicationCatalogAsync(int id, ApplicationCatalogUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"ApplicationCatalog with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(updated);
        }

        public async Task<bool> DeleteApplicationCatalogAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<ApplicationCatalogReturnDto> GetApplicationCatalogByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"ApplicationCatalog with Id {id} not found.");

            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllApplicationCatalogAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<ApplicationCatalogListItemDto>>(items);

            return new PagedResponse<ApplicationCatalogListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };

        }
    }
}
