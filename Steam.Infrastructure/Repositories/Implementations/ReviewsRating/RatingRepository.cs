using Steam.Domain.Entities.ReviewsRating;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;

namespace Steam.Infrastructure.Repositories.Implementations.ReviewsRating
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
