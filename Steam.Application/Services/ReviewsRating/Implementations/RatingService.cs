using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Rating;
using Steam.Application.Services.ReviewsRating.Interfaces;
using Steam.Domain.Entities.ReviewsRating;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.ReviewsRating.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating> _repository;
        private readonly IMapper _mapper;

        public RatingService(IRepository<Rating> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RatingReturnDto> CreateRatingAsync(RatingCreateDto dto)
        {
            var entity = _mapper.Map<Rating>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<RatingReturnDto>(created);
        }

        public async Task<RatingReturnDto> UpdateRatingAsync(int id, RatingUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Rating with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<RatingReturnDto>(updated);
        }

        public async Task<bool> DeleteRatingAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity); // default soft delete
        }

        public async Task<RatingReturnDto> GetRatingByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Rating with Id {id} not found.");

            return _mapper.Map<RatingReturnDto>(entity);
        }

        public async Task<PagedResponse<RatingListItemDto>> GetAllRatingsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mappedItems = _mapper.Map<List<RatingListItemDto>>(items);

            return new PagedResponse<RatingListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
