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
    }
}
