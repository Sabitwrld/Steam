using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.ReviewsRating;
using System.Linq.Expressions;

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
                .Include(r => r.User)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.HelpfulCount)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(IEnumerable<T>, int)> GetReviewsForApplicationProjectedAsync<T>(
            int applicationId, int pageNumber, int pageSize, Expression<Func<Review, T>> selector)
        {
            var query = _dbSet
                .Where(r => r.ApplicationId == applicationId)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var projected = await query
                .OrderByDescending(r => r.HelpfulCount)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync();

            return (projected, totalCount);
        }
    }
}