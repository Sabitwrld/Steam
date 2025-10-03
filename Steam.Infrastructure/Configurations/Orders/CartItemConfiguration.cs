using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Orders
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(ci => ci.Id);

            // Prevents adding the same ApplicationId to the same cart more than once as a separate item
            builder.HasIndex(ci => new { ci.CartId, ci.ApplicationId }).IsUnique();

            // Relationship to ApplicationCatalog
            builder.HasOne(ci => ci.Application)
                   .WithMany()
                   .HasForeignKey(ci => ci.ApplicationId)
                   .OnDelete(DeleteBehavior.Restrict); // Do not delete an application if it is in a cart

            builder.HasQueryFilter(ci => !ci.IsDeleted);
        }
    }
}
