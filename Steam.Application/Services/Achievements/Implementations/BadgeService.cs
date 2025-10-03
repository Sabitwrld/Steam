using AutoMapper;
using Steam.Application.DTOs.Achievements.Badge;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Achievements.Implementations
{
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<Badge> _repository;
        private readonly IMapper _mapper;

        public BadgeService(IRepository<Badge> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BadgeReturnDto> CreateBadgeAsync(BadgeCreateDto dto)
        {
            var entity = _mapper.Map<Badge>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<BadgeReturnDto>(created);
        }

        public async Task<BadgeReturnDto> UpdateBadgeAsync(int id, BadgeUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Badge {id} not found");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<BadgeReturnDto>(updated);
        }

        public async Task<bool> DeleteBadgeAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<BadgeReturnDto> GetBadgeByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Badge {id} not found");

            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<PagedResponse<BadgeListItemDto>> GetAllBadgesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mapped = _mapper.Map<List<BadgeListItemDto>>(items);

            return new PagedResponse<BadgeListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mapped
            };
        }
    }
}
