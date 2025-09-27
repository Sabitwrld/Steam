using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities;
using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ApplicationCatalog> Applications { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Media> Media { get; set; } = default!;
        public DbSet<SystemRequirements> SystemRequirements { get; set; } = default!;
        public DbSet<Tag> Tags { get; set; } = default!;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
