using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Library
{
    public class UserLibrary : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
