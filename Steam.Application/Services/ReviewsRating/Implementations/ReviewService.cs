using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Application.Exceptions;
using Steam.Application.Services.ReviewsRating.Interfaces;
using Steam.Domain.Entities.ReviewsRating;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.ReviewsRating.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepo;
        private readonly IMapper _mapper;

        public ReviewService(IRepository<Review> reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<ReviewReturnDto> CreateReviewAsync(ReviewCreateDto dto)
        {
            // Check if the user has already reviewed this application
            var existingReview = await _reviewRepo.IsExistsAsync(r => r.UserId == dto.UserId && r.ApplicationId == dto.ApplicationId);
            if (existingReview)
            {
                throw new Exception("You have already submitted a review for this application.");
            }

            var entity = _mapper.Map<Review>(dto);
            await _reviewRepo.CreateAsync(entity);

            // Re-fetch with user details to return the correct DTO with UserName
            return await GetReviewByIdAsync(entity.Id);
        }

        public async Task<ReviewReturnDto> UpdateReviewAsync(int reviewId, string userId, ReviewUpdateDto dto)
        {
            var entity = await _reviewRepo.GetByIdAsync(reviewId);

            if (entity == null)
                throw new NotFoundException(nameof(Review), reviewId);

            // Security check: ensure the user making the request is the owner of the review
            if (entity.UserId != userId)
                throw new Exception("You are not authorized to edit this review.");

            _mapper.Map(dto, entity);
            await _reviewRepo.UpdateAsync(entity);

            return await GetReviewByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
        {
            var entity = await _reviewRepo.GetByIdAsync(reviewId);
            if (entity == null)
                return false;

            // Security check: ensure the user making the request is the owner of the review
            if (entity.UserId != userId)
                throw new Exception("You are not authorized to delete this review.");

            return await _reviewRepo.DeleteAsync(entity);
        }

        public async Task<ReviewReturnDto> GetReviewByIdAsync(int id)
        {
            var entity = await _reviewRepo.GetEntityAsync(
                predicate: r => r.Id == id,
                includes: new Func<IQueryable<Review>, IQueryable<Review>>[] { q => q.Include(r => r.User) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(Review), id);

            return _mapper.Map<ReviewReturnDto>(entity);
        }

        public async Task<PagedResponse<ReviewListItemDto>> GetReviewsForApplicationAsync(int applicationId, int pageNumber, int pageSize)
        {
            var query = _reviewRepo.GetQuery(r => r.ApplicationId == applicationId, asNoTracking: true)
                                   .Include(r => r.User);

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(r => r.HelpfulCount)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            // The logic for shortening content is now handled by AutoMapper, so the loop is removed.
            var mappedItems = _mapper.Map<List<ReviewListItemDto>>(items);

            return new PagedResponse<ReviewListItemDto>
            {
                Data = mappedItems, // Just use the directly mapped items
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        public async Task MarkAsHelpfulAsync(int reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null)
                throw new NotFoundException(nameof(Review), reviewId);

            review.HelpfulCount++;
            await _reviewRepo.UpdateAsync(review);
        }

        public async Task MarkAsFunnyAsync(int reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null)
                throw new NotFoundException(nameof(Review), reviewId);

            review.FunnyCount++;
            await _reviewRepo.UpdateAsync(review);
        }
    }
}