using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;

namespace Steam.Infrastructure.Repositories.Implementations.Achievements
{
    public class CraftingRecipeRepository : Repository<CraftingRecipe>, ICraftingRecipeRepository
    {
        public CraftingRecipeRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<CraftingRecipe> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(cr => cr.ResultBadge).AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
