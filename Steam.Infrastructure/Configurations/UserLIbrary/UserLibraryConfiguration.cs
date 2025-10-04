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
    public class UserLibraryConfiguration : IEntityTypeConfiguration<UserLibrary>
    {
        public void Configure(EntityTypeBuilder<UserLibrary> builder)
        {
            builder.ToTable("UserLibraries");
            builder.HasKey(ul => ul.Id);

            // Each user has only one library, so UserId should be unique.
            builder.HasIndex(ul => ul.UserId).IsUnique();

            // A UserLibrary has many Licenses.
            builder.HasMany(ul => ul.Licenses)
                   .WithOne(l => l.UserLibrary)
                   .HasForeignKey(l => l.UserLibraryId);

            // Apply soft delete query filter.
            builder.HasQueryFilter(ul => !ul.IsDeleted);
        }
    }
}
