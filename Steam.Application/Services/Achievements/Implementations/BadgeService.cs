using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public BadgeService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BadgeReturnDto> CreateBadgeAsync(BadgeCreateDto dto)
        {
            var entity = _mapper.Map<Badge>(dto);
            await _unitOfWork.BadgeRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<BadgeReturnDto> UpdateBadgeAsync(int id, BadgeUpdateDto dto)
        {
            var entity = await _unitOfWork.BadgeRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Badge), id);

            _mapper.Map(dto, entity);
            _unitOfWork.BadgeRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<bool> DeleteBadgeAsync(int id)
        {
            var entity = await _unitOfWork.BadgeRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.BadgeRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<BadgeReturnDto> GetBadgeByIdAsync(int id)
        {
            var entity = await _unitOfWork.BadgeRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Badge), id);

            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<PagedResponse<BadgeListItemDto>> GetAllBadgesAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.BadgeRepository.GetAllPagedAsync(pageNumber, pageSize);

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
