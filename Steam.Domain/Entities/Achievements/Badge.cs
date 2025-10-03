using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Achievements
{
    public class Badge : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
