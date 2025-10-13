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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public LeaderboardService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LeaderboardReturnDto> AddOrUpdateScoreAsync(LeaderboardCreateDto dto)
        {
            var existingEntry = await _unitOfWork.LeaderboardRepository.GetEntityAsync(l => l.UserId == dto.UserId && l.ApplicationId == dto.ApplicationId);
            int entryId;

            if (existingEntry != null)
            {
                if (dto.Score > existingEntry.Score)
                {
                    existingEntry.Score = dto.Score;
                    _unitOfWork.LeaderboardRepository.Update(existingEntry);
                }
                entryId = existingEntry.Id;
            }
            else
            {
                var newEntry = _mapper.Map<Leaderboard>(dto);
                await _unitOfWork.LeaderboardRepository.CreateAsync(newEntry);
                entryId = newEntry.Id; // ID-ni əldə etmək üçün
            }

            await _unitOfWork.CommitAsync();
            return await GetScoreByIdAsync(entryId);
        }

        public async Task<bool> DeleteScoreAsync(int id)
        {
            var entity = await _unitOfWork.LeaderboardRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.LeaderboardRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<LeaderboardReturnDto> GetScoreByIdAsync(int id)
        {
            var entity = await _unitOfWork.LeaderboardRepository.GetEntityAsync(
                predicate: l => l.Id == id,
                includes: new Func<IQueryable<Leaderboard>, IQueryable<Leaderboard>>[] { q => q.Include(l => l.User) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(Leaderboard), id);

            return _mapper.Map<LeaderboardReturnDto>(entity);
        }

        public async Task<PagedResponse<LeaderboardListItemDto>> GetLeaderboardForApplicationAsync(int applicationId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.LeaderboardRepository.GetQuery(l => l.ApplicationId == applicationId, asNoTracking: true)
                                   .Include(l => l.User)
                                   .OrderByDescending(l => l.Score);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var mappedItems = _mapper.Map<List<LeaderboardListItemDto>>(items);

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

