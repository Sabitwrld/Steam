using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Achievements.Achievements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Achievements.Implementations
{
    public class AchievementService : IAchievementService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public AchievementService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AchievementReturnDto> CreateAchievementAsync(AchievementCreateDto dto)
        {
            var entity = _mapper.Map<Achievement>(dto);
            await _unitOfWork.AchievementRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<AchievementReturnDto> UpdateAchievementAsync(int id, AchievementUpdateDto dto)
        {
            var entity = await _unitOfWork.AchievementRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Achievement), id);

            _mapper.Map(dto, entity);
            _unitOfWork.AchievementRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<bool> DeleteAchievementAsync(int id)
        {
            var entity = await _unitOfWork.AchievementRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.AchievementRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<AchievementReturnDto> GetAchievementByIdAsync(int id)
        {
            var entity = await _unitOfWork.AchievementRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Achievement), id);

            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<PagedResponse<AchievementListItemDto>> GetAllAchievementsAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.AchievementRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<AchievementListItemDto>
            {
                Data = _mapper.Map<List<AchievementListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
