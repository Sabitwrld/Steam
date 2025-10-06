using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Achievements.Badge;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
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
            await _repository.CreateAsync(entity);
            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<BadgeReturnDto> UpdateBadgeAsync(int id, BadgeUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Badge), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<BadgeReturnDto>(entity);
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
            if (entity == null)
                throw new NotFoundException(nameof(Badge), id);

            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<PagedResponse<BadgeListItemDto>> GetAllBadgesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<BadgeListItemDto>
            {
                Data = _mapper.Map<List<BadgeListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
