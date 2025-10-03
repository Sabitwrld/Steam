using Steam.Domain.Entities.ReviewsRating;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;

namespace Steam.Infrastructure.Repositories.Implementations.ReviewsRating
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
