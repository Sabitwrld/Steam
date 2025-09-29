using AutoMapper;
using Steam.Application.DTOs.Library.License;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Library.Interfaces;
using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Library.Implementations
{
    public class LicenseService : ILicenseService
    {
        private readonly IRepository<License> _repository;
        private readonly IMapper _mapper;

        public LicenseService(IRepository<License> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LicenseReturnDto> CreateLicenseAsync(int libraryId, LicenseCreateDto dto)
        {
            var entity = _mapper.Map<License>(dto);
            entity.UserLibraryId = libraryId;

            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<LicenseReturnDto>(created);
        }

        public async Task<LicenseReturnDto> UpdateLicenseAsync(int id, LicenseUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"License with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<LicenseReturnDto>(updated);
        }

        public async Task<bool> DeleteLicenseAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<LicenseReturnDto> GetLicenseByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"License with Id {id} not found.");

            return _mapper.Map<LicenseReturnDto>(entity);
        }

        public async Task<PagedResponse<LicenseListItemDto>> GetAllLicensesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<LicenseListItemDto>>(items);

            return new PagedResponse<LicenseListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
