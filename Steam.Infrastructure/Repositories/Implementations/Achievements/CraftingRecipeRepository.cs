using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.Achievements
{
    public class CraftingRecipeRepository : Repository<CraftingRecipe>, ICraftingRecipeRepository
    {
        public CraftingRecipeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
