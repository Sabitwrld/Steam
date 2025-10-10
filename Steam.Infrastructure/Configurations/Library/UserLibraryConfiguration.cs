using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Library;

namespace Steam.Infrastructure.Configurations.Library
{
    public class UserLibraryConfiguration : IEntityTypeConfiguration<UserLibrary>
    {
        public void Configure(EntityTypeBuilder<UserLibrary> builder)
        {
            builder.ToTable("UserLibraries");
            builder.HasKey(ul => ul.Id);

            builder.HasIndex(ul => ul.UserId).IsUnique();

            builder.HasMany(ul => ul.Licenses)
                   .WithOne(l => l.UserLibrary)
                   .HasForeignKey(l => l.UserLibraryId);

            // AppUser ilə əlaqə və silmə davranışının təyin edilməsi
            builder.HasOne(ul => ul.User)
                   .WithMany()
                   .HasForeignKey(ul => ul.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(ul => !ul.IsDeleted);
        }
    }
}
