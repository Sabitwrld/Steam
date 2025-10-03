using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Store
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Percent).HasColumnType("decimal(5, 2)");

            builder.HasOne(d => d.Campaign)
                   .WithMany(c => c.Discounts)
                   .HasForeignKey(d => d.CampaignId)
                   .IsRequired(false);

            builder.HasOne(d => d.Application)
                   .WithMany()
                   .HasForeignKey(d => d.ApplicationId);

            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}
