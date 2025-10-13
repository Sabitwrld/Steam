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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public LicenseService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LicenseReturnDto> AddLicenseAsync(int userLibraryId, LicenseCreateDto dto)
        {
            var existing = await _unitOfWork.LicenseRepository.IsExistsAsync(l => l.UserLibraryId == userLibraryId && l.ApplicationId == dto.ApplicationId);
            if (existing)
            {
                // Artıq mövcud lisenziya varsa, xəta atmaq əvəzinə, sadəcə mövcud lisenziyanı qaytara bilərik.
                // Və ya heç bir şey etmədən geri dönə bilərik. Bu, biznes məntiqindən asılıdır.
                // Hazırkı implementasiyada xəta atmaq daha məqsədəuyğundur.
                throw new Exception("User already owns this application.");
            }

            var entity = _mapper.Map<License>(dto);
            entity.UserLibraryId = userLibraryId;

            await _unitOfWork.LicenseRepository.CreateAsync(entity);
            // DİQQƏT: Burada CommitAsync çağırılmır! Tranzaksiyanı PaymentService və ya GiftService idarə edəcək.

            // DTO qaytarmaq üçün yenidən sorğu etməyə ehtiyac yoxdur, çünki ID hələ təyin edilməyib.
            // Bu metodun nəticəsi dərhal istifadə edilmədiyi üçün bu kifayətdir.
            return _mapper.Map<LicenseReturnDto>(entity);
        }

        public async Task<LicenseReturnDto> UpdateLicenseAsync(int id, LicenseUpdateDto dto)
        {
            var entity = await _unitOfWork.LicenseRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(License), id);

            _mapper.Map(dto, entity);
            _unitOfWork.LicenseRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return await GetLicenseByIdAsync(id);
        }

        public async Task<bool> DeleteLicenseAsync(int id)
        {
            var entity = await _unitOfWork.LicenseRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.LicenseRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<LicenseReturnDto> GetLicenseByIdAsync(int id)
        {
            var entity = await _unitOfWork.LicenseRepository.GetEntityAsync(
                predicate: l => l.Id == id,
                includes: new Func<IQueryable<License>, IQueryable<License>>[] { q => q.Include(l => l.Application) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(License), id);

            return _mapper.Map<LicenseReturnDto>(entity);
        }
    }
}
