namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    public record CraftingRecipeListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string ResultBadgeName { get; init; } = default!;
    }
}
