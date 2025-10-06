using Steam.Application.DTOs.Achievements.Badge;

namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    /// <summary>
    /// Full details of a crafting recipe, including the result and required badges.
    /// </summary>
    public record CraftingRecipeReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public BadgeReturnDto ResultBadge { get; init; } = default!;
        public List<BadgeListItemDto> Requirements { get; init; } = new();
    }
}
