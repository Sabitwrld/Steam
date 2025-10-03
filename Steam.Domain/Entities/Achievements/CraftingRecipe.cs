using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Achievements
{
    public class CraftingRecipe : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> RequiredBadgeIds { get; set; } = new();
        public int ResultBadgeId { get; set; }
    }
}
