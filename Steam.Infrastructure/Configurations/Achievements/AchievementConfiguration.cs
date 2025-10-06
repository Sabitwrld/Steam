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
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.ToTable("Achievements");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.IconUrl).HasMaxLength(500);

            builder.HasOne(a => a.Application)
                   .WithMany() // Assuming ApplicationCatalog does not need a collection of Achievements
                   .HasForeignKey(a => a.ApplicationId);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
