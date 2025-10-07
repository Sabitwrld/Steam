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
