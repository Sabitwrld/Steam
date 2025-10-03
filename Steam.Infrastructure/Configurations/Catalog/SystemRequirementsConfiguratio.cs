using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Configurations.Catalog
{
    public class SystemRequirementsConfiguration : IEntityTypeConfiguration<SystemRequirements>
    {
        public void Configure(EntityTypeBuilder<SystemRequirements> builder)
        {
            builder.ToTable("SystemRequirements");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.RequirementType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.OS)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.CPU)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.GPU)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.RAM)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Storage)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.AdditionalNotes)
                .HasMaxLength(500);

            builder.HasOne(s => s.Application)
                .WithMany(a => a.SystemRequirements)
                .HasForeignKey(s => s.ApplicationId);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
