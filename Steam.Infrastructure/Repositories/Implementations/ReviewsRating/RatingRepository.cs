using Steam.Domain.Entities.ReviewsRating;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.ReviewsRating
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
