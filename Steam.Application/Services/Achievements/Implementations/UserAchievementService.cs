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
        private readonly IRepository<UserAchievement> _repository;
        private readonly IMapper _mapper;

        public UserAchievementService(IRepository<UserAchievement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserAchievementReturnDto> UnlockAchievementAsync(UserAchievementCreateDto dto)
        {
            var existing = await _repository.IsExistsAsync(ua => ua.UserId == dto.UserId && ua.AchievementId == dto.AchievementId);
            if (existing)
            {
                throw new Exception("User has already unlocked this achievement.");
            }

            var entity = _mapper.Map<UserAchievement>(dto);
            entity.DateUnlocked = DateTime.UtcNow;

            await _repository.CreateAsync(entity);
            return await GetUserAchievementByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteUserAchievementAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<UserAchievementReturnDto> GetUserAchievementByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                predicate: ua => ua.Id == id,
                includes: new Func<IQueryable<UserAchievement>, IQueryable<UserAchievement>>[] { q => q.Include(ua => ua.Achievement) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(UserAchievement), id);

            return _mapper.Map<UserAchievementReturnDto>(entity);
        }

        public async Task<PagedResponse<UserAchievementListItemDto>> GetAchievementsForUserAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(ua => ua.UserId == userId, asNoTracking: true)
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
