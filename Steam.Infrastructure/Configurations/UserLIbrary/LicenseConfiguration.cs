using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.UserLIbrary
{
    public class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            builder.ToTable("Licenses");
            builder.HasKey(l => l.Id);

            // To prevent a user from having more than one license for the same game
            builder.HasIndex(l => new { l.UserLibraryId, l.ApplicationId }).IsUnique();

            builder.Property(l => l.LicenseType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(l => l.PlaytimeInMinutes)
                   .HasDefaultValue(0);

            builder.Property(l => l.IsHidden)
                   .HasDefaultValue(false);

            // Relationship to ApplicationCatalog
            builder.HasOne(l => l.Application)
                   .WithMany() // Assuming ApplicationCatalog does not need a collection of Licenses
                   .HasForeignKey(l => l.ApplicationId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevents deleting a game if it exists in a user's library

            // Apply soft delete query filter.
            builder.HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
