using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

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

        public async Task<LeaderboardReturnDto> AddOrUpdateScoreAsync(LeaderboardCreateDto dto)
        {
            var existingEntry = await _repository.GetEntityAsync(l => l.UserId == dto.UserId && l.ApplicationId == dto.ApplicationId);

            if (existingEntry != null)
            {
                // Update score only if the new score is higher
                if (dto.Score > existingEntry.Score)
                {
                    existingEntry.Score = dto.Score;
                    await _repository.UpdateAsync(existingEntry);
                }
                return await GetScoreByIdAsync(existingEntry.Id);
            }
            else
            {
                var newEntry = _mapper.Map<Leaderboard>(dto);
                await _repository.CreateAsync(newEntry);
                return await GetScoreByIdAsync(newEntry.Id);
            }
        }

        public async Task<bool> DeleteScoreAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<LeaderboardReturnDto> GetScoreByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                predicate: l => l.Id == id,
                includes: new System.Func<IQueryable<Leaderboard>, IQueryable<Leaderboard>>[] { q => q.Include(l => l.User) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(Leaderboard), id);

            return _mapper.Map<LeaderboardReturnDto>(entity);
        }

        public async Task<PagedResponse<LeaderboardListItemDto>> GetLeaderboardForApplicationAsync(int applicationId, int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(l => l.ApplicationId == applicationId, asNoTracking: true)
                                   .Include(l => l.User)
                                   .OrderByDescending(l => l.Score);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var mappedItems = _mapper.Map<List<LeaderboardListItemDto>>(items);

            // Set the rank for each item
            for (int i = 0; i < mappedItems.Count; i++)
            {
                mappedItems[i].Rank = ((pageNumber - 1) * pageSize) + i + 1;
            }

            return new PagedResponse<LeaderboardListItemDto>
            {
                Data = mappedItems,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}

