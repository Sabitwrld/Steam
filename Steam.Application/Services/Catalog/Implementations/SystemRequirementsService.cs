using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class SystemRequirementsService : ISystemRequirementsService
    {
        private readonly IRepository<SystemRequirements> _repo;
        private readonly IMapper _mapper;

        public SystemRequirementsService(IRepository<SystemRequirements> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SystemRequirementsReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id, q => q.Include(s => s.Application));
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<PagedResponse<SystemRequirementsListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<SystemRequirementsListItemDto>
            {
                Data = _mapper.Map<List<SystemRequirementsListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        public async Task<SystemRequirementsReturnDto> CreateAsync(SystemRequirementsCreateDto dto)
        {
            var entity = _mapper.Map<SystemRequirements>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<SystemRequirementsReturnDto> UpdateAsync(int id, SystemRequirementsUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("SystemRequirements not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }
    }
}
