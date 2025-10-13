using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Achievements.Implementations
{
    public class UserAchievementService : IUserAchievementService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public UserAchievementService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserAchievementReturnDto> UnlockAchievementAsync(UserAchievementCreateDto dto)
        {
            var existing = await _unitOfWork.UserAchievementRepository.IsExistsAsync(ua => ua.UserId == dto.UserId && ua.AchievementId == dto.AchievementId);
            if (existing)
            {
                throw new Exception("User has already unlocked this achievement.");
            }

            var entity = _mapper.Map<UserAchievement>(dto);
            entity.DateUnlocked = DateTime.UtcNow;

            await _unitOfWork.UserAchievementRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return await GetUserAchievementByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteUserAchievementAsync(int id)
        {
            var entity = await _unitOfWork.UserAchievementRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.UserAchievementRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<UserAchievementReturnDto> GetUserAchievementByIdAsync(int id)
        {
            var entity = await _unitOfWork.UserAchievementRepository.GetEntityAsync(
                predicate: ua => ua.Id == id,
                includes: new Func<IQueryable<UserAchievement>, IQueryable<UserAchievement>>[] { q => q.Include(ua => ua.Achievement) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(UserAchievement), id);

            return _mapper.Map<UserAchievementReturnDto>(entity);
        }

        public async Task<PagedResponse<UserAchievementListItemDto>> GetAchievementsForUserAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.UserAchievementRepository.GetQuery(ua => ua.UserId == userId, asNoTracking: true)
                                   .Include(ua => ua.Achievement);

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(ua => ua.DateUnlocked)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            return new PagedResponse<UserAchievementListItemDto>
            {
                Data = _mapper.Map<List<UserAchievementListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
