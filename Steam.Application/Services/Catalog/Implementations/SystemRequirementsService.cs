using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class SystemRequirementsService : ISystemRequirementsService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public SystemRequirementsService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SystemRequirementsReturnDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.SystemRequirementsRepository.GetByIdAsync(id, q => q.Include(s => s.Application));
            if (entity == null) throw new NotFoundException(nameof(SystemRequirements), id);
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<PagedResponse<SystemRequirementsListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.SystemRequirementsRepository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var entities = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<SystemRequirementsListItemDto>
            {
                Data = _mapper.Map<List<SystemRequirementsListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<SystemRequirementsReturnDto> CreateAsync(SystemRequirementsCreateDto dto)
        {
            var entity = _mapper.Map<SystemRequirements>(dto);
            await _unitOfWork.SystemRequirementsRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<SystemRequirementsReturnDto> UpdateAsync(int id, SystemRequirementsUpdateDto dto)
        {
            var entity = await _unitOfWork.SystemRequirementsRepository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException(nameof(SystemRequirements), id);

            _mapper.Map(dto, entity);
            _unitOfWork.SystemRequirementsRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SystemRequirementsReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.SystemRequirementsRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.SystemRequirementsRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
