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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewReturnDto> CreateReviewAsync(ReviewCreateDto dto)
        {
            var existingReview = await _unitOfWork.ReviewRepository.IsExistsAsync(r => r.UserId == dto.UserId && r.ApplicationId == dto.ApplicationId);
            if (existingReview)
            {
                throw new Exception("You have already submitted a review for this application.");
            }

            var entity = _mapper.Map<Review>(dto);
            await _unitOfWork.ReviewRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();

            return await GetReviewByIdAsync(entity.Id);
        }

        public async Task<ReviewReturnDto> UpdateReviewAsync(int reviewId, string userId, ReviewUpdateDto dto)
        {
            var entity = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);

            if (entity == null)
                throw new NotFoundException(nameof(Review), reviewId);

            if (entity.UserId != userId)
                throw new Exception("You are not authorized to edit this review.");

            _mapper.Map(dto, entity);
            _unitOfWork.ReviewRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return await GetReviewByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
        {
            var entity = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (entity == null)
                return false;

            if (entity.UserId != userId)
                throw new Exception("You are not authorized to delete this review.");

            _unitOfWork.ReviewRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<ReviewReturnDto> GetReviewByIdAsync(int id)
        {
            var entity = await _unitOfWork.ReviewRepository.GetEntityAsync(
                predicate: r => r.Id == id,
                includes: new Func<IQueryable<Review>, IQueryable<Review>>[] { q => q.Include(r => r.User) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(Review), id);

            return _mapper.Map<ReviewReturnDto>(entity);
        }

        public async Task<PagedResponse<ReviewListItemDto>> GetReviewsForApplicationAsync(
            int applicationId, int pageNumber, int pageSize)
        {
            // Sorğu məntiqi Repository-yə daşındı
            var (items, totalCount) = await _unitOfWork.ReviewRepository
                .GetReviewsByApplicationIdPagedAsync(applicationId, pageNumber, pageSize);

            var mappedItems = _mapper.Map<List<ReviewListItemDto>>(items);

            return new PagedResponse<ReviewListItemDto>
            {
                Data = mappedItems,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        public async Task MarkAsHelpfulAsync(int reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                throw new NotFoundException(nameof(Review), reviewId);

            review.HelpfulCount++;
            _unitOfWork.ReviewRepository.Update(review);
            await _unitOfWork.CommitAsync();
        }

        public async Task MarkAsFunnyAsync(int reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                throw new NotFoundException(nameof(Review), reviewId);

            review.FunnyCount++;
            _unitOfWork.ReviewRepository.Update(review);
            await _unitOfWork.CommitAsync();
        }
    }
}