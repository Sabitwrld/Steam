using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities;
using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System.Linq.Expressions;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Bütün BaseEntity-ləri soft delete üçün filterləyirik
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

    }
}
