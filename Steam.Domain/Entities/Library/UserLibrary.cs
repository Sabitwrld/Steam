using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Library
{
    public class UserLibrary : BaseEntity
    {
        public string UserId { get; set; }
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
