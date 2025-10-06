using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Achievements
{
    public class CraftingRecipe : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int ResultBadgeId { get; set; }

        public Badge ResultBadge { get; set; } = default!;
        public ICollection<CraftingRecipeRequirement> Requirements { get; set; } = new List<CraftingRecipeRequirement>();
    }
}
