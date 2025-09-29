using AutoMapper;
using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<UserAchievementReturnDto> CreateUserAchievementAsync(UserAchievementCreateDto dto)
        {
            var entity = _mapper.Map<UserAchievement>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<UserAchievementReturnDto>(created);
        }

        public async Task<UserAchievementReturnDto> UpdateUserAchievementAsync(int id, UserAchievementUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"UserAchievement {id} not found");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserAchievementReturnDto>(updated);
        }

        public async Task<bool> DeleteUserAchievementAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<UserAchievementReturnDto> GetUserAchievementByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"UserAchievement {id} not found");

            return _mapper.Map<UserAchievementReturnDto>(entity);
        }

        public async Task<PagedResponse<UserAchievementListItemDto>> GetAllUserAchievementsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mapped = _mapper.Map<List<UserAchievementListItemDto>>(items);

            return new PagedResponse<UserAchievementListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mapped
            };
        }
    }
}
