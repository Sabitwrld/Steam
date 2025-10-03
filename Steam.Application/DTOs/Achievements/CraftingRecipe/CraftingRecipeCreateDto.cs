namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    public record CraftingRecipeCreateDto
    {
        public string Name { get; init; }
        public List<int> RequiredBadgeIds { get; init; }
        public int ResultBadgeId { get; init; }
    }
}
