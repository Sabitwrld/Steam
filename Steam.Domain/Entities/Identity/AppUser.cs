using Microsoft.AspNetCore.Identity;

namespace Steam.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
