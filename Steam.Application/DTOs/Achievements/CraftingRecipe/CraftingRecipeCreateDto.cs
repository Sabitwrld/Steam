namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    /// <summary>
    /// Data needed to create a new crafting recipe.
    /// </summary>
    public record CraftingRecipeCreateDto
    {
        public string Name { get; init; } = default!;
        /// <summary>
        /// The ID of the badge that will be created when this recipe is used.
        /// </summary>
        public int ResultBadgeId { get; init; }
        /// <summary>
        /// A list of badge IDs that are required to craft the result badge.
        /// </summary>
        public List<int> RequiredBadgeIds { get; init; } = new();
    }
}
