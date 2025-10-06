using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Achievements
{
    public class CraftingRecipeRequirement
    {
        public int CraftingRecipeId { get; set; }
        public CraftingRecipe CraftingRecipe { get; set; } = default!;

        public int RequiredBadgeId { get; set; }
        public Badge RequiredBadge { get; set; } = default!;
    }
}
