using AutoMapper;
using Steam.Application.DTOs.Achievements.Leaderboard;
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
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IRepository<Leaderboard> _repository;
        private readonly IMapper _mapper;

        public LeaderboardService(IRepository<Leaderboard> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LeaderboardReturnDto> CreateLeaderboardAsync(LeaderboardCreateDto dto)
        {
            var entity = _mapper.Map<Leaderboard>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<LeaderboardReturnDto>(created);
        }

        public async Task<LeaderboardReturnDto> UpdateLeaderboardAsync(int id, LeaderboardUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Leaderboard {id} not found");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<LeaderboardReturnDto>(updated);
        }

        public async Task<bool> DeleteLeaderboardAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<LeaderboardReturnDto> GetLeaderboardByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Leaderboard {id} not found");

            return _mapper.Map<LeaderboardReturnDto>(entity);
        }

        public async Task<PagedResponse<LeaderboardListItemDto>> GetAllLeaderboardsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mapped = _mapper.Map<List<LeaderboardListItemDto>>(items);

            return new PagedResponse<LeaderboardListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mapped
            };
        }
    }
}

