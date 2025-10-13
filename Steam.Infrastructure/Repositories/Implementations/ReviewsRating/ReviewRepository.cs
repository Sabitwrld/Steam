using Microsoft.EntityFrameworkCore;
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

        public async Task<(IEnumerable<Review> Items, int TotalCount)> GetReviewsByApplicationIdPagedAsync(
            int applicationId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(r => r.ApplicationId == applicationId)
                .Include(r => r.User) // User məlumatlarını da gətirmək üçün
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.HelpfulCount) // Servisdəki sıralama məntiqi bura daşındı
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
