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
        private readonly IRepository<Achievement> _repository;
        private readonly IMapper _mapper;

        public AchievementService(IRepository<Achievement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AchievementReturnDto> CreateAchievementAsync(AchievementCreateDto dto)
        {
            var entity = _mapper.Map<Achievement>(dto);
            await _repository.CreateAsync(entity);
            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<AchievementReturnDto> UpdateAchievementAsync(int id, AchievementUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Achievement), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<bool> DeleteAchievementAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<AchievementReturnDto> GetAchievementByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Achievement), id);

            return _mapper.Map<AchievementReturnDto>(entity);
        }

        public async Task<PagedResponse<AchievementListItemDto>> GetAllAchievementsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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
