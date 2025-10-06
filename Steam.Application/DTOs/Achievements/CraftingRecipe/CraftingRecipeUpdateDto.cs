namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    public record CraftingRecipeUpdateDto
    {
        public string Name { get; init; } = default!;
        public int ResultBadgeId { get; init; }
        public List<int> RequiredBadgeIds { get; init; } = new();
    }
}
