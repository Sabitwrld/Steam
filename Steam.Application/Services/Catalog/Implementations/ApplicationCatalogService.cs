using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<ApplicationCatalog> _repo;
        private readonly IMapper _mapper;

        public ApplicationCatalogService(IRepository<ApplicationCatalog> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApplicationCatalogReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id,
                q => q.Include(a => a.Genres),
                q => q.Include(a => a.Tags),
                q => q.Include(a => a.Media),
                q => q.Include(a => a.SystemRequirements));
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<ApplicationCatalogListItemDto>
            {
                Data = _mapper.Map<List<ApplicationCatalogListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        public async Task<ApplicationCatalogReturnDto> CreateAsync(ApplicationCatalogCreateDto dto)
        {
            var entity = _mapper.Map<ApplicationCatalog>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<ApplicationCatalogReturnDto> UpdateAsync(int id, ApplicationCatalogUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Application not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }
    }
}
