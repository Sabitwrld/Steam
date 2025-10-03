namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    public record CraftingRecipeUpdateDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<int> RequiredBadgeIds { get; init; }
        public int ResultBadgeId { get; init; }
    }
}
