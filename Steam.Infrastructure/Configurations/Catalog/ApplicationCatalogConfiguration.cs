using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Catalog
{
    public class ApplicationCatalogConfiguration : IEntityTypeConfiguration<ApplicationCatalog>
    {
        public void Configure(EntityTypeBuilder<ApplicationCatalog> builder)
        {
            builder.ToTable("ApplicationCatalogs");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .HasMaxLength(2000);

            builder.Property(a => a.Developer)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Publisher)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.ApplicationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(a => a.Media)
                .WithOne(m => m.Application)
                .HasForeignKey(m => m.ApplicationId);

            builder.HasMany(a => a.SystemRequirements)
                .WithOne(s => s.Application)
                .HasForeignKey(s => s.ApplicationId);

            builder.HasMany(a => a.Genres)
                .WithMany(g => g.Applications);

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Applications);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }

}
