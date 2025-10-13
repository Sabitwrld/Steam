using Steam.Domain.Entities.ReviewsRating;

namespace Steam.Infrastructure.Repositories.Interfaces.ReviewsRating
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<(IEnumerable<Review> Items, int TotalCount)> GetReviewsByApplicationIdPagedAsync(
           int applicationId,
           int pageNumber,
           int pageSize);
    }
}
