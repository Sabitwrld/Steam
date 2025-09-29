using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;
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
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;
        private readonly IMapper _mapper;

        public ReviewService(IRepository<Review> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReviewReturnDto> CreateReviewAsync(ReviewCreateDto dto)
        {
            var entity = _mapper.Map<Review>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<ReviewReturnDto>(created);
        }

        public async Task<ReviewReturnDto> UpdateReviewAsync(int id, ReviewUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Review with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ReviewReturnDto>(updated);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity); // default soft delete
        }

        public async Task<ReviewReturnDto> GetReviewByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Review with Id {id} not found.");

            return _mapper.Map<ReviewReturnDto>(entity);
        }

        public async Task<PagedResponse<ReviewListItemDto>> GetAllReviewsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mappedItems = _mapper.Map<List<ReviewListItemDto>>(items);

            return new PagedResponse<ReviewListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}