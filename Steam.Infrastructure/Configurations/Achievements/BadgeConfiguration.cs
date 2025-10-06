using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Achievements
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.ToTable("Badges");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(150);
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.IconUrl).HasMaxLength(500);

            builder.HasOne(b => b.Application)
                   .WithMany()
                   .HasForeignKey(b => b.ApplicationId);

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
