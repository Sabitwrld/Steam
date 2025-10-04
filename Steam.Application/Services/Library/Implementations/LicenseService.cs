using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Library.License;
using Steam.Application.Exceptions;
using Steam.Application.Services.Library.Interfaces;
using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Repositories.Interfaces;

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

        public async Task<LicenseReturnDto> AddLicenseAsync(int userLibraryId, LicenseCreateDto dto)
        {
            // Check if this license already exists
            var existing = await _repository.IsExistsAsync(l => l.UserLibraryId == userLibraryId && l.ApplicationId == dto.ApplicationId);
            if (existing)
            {
                throw new Exception("User already owns this application.");
            }

            var entity = _mapper.Map<License>(dto);
            entity.UserLibraryId = userLibraryId;

            await _repository.CreateAsync(entity);

            // Re-fetch with Application details for the return DTO
            var createdEntity = await GetLicenseByIdAsync(entity.Id);
            return createdEntity;
        }

        public async Task<LicenseReturnDto> UpdateLicenseAsync(int id, LicenseUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(License), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return await GetLicenseByIdAsync(id);
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
            var entity = await _repository.GetEntityAsync(
                predicate: l => l.Id == id,
                includes: new Func<IQueryable<License>, IQueryable<License>>[] { q => q.Include(l => l.Application) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(License), id);

            return _mapper.Map<LicenseReturnDto>(entity);
        }
    }
}
