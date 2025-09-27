using Microsoft.AspNetCore.Identity;

namespace Steam.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
