using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities;
using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Orders;
using Steam.Domain.Entities.Store;
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

        public DbSet<PricePoint> PricePoints { get; set; } = default!;
        public DbSet<RegionalPrice> RegionalPrices { get; set; } = default!;
        public DbSet<Discount> Discounts { get; set; } = default!;
        public DbSet<Campaign> Campaigns { get; set; } = default!;
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DbSet<Gift> Gifts { get; set; } = default!;
        public DbSet<Voucher> Vouchers { get; set; } = default!;
        public DbSet<Wishlist> Wishlists { get; set; } = default!;

        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Refund> Refunds { get; set; } = default!;


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
