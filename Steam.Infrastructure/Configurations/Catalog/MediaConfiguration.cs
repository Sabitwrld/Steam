using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Configurations.Catalog
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Media");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.MediaType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Order)
                .IsRequired();

            builder.HasOne(m => m.Application)
                .WithMany(a => a.Media)
                .HasForeignKey(m => m.ApplicationId);

            builder.HasQueryFilter(m => !m.IsDeleted);
        }
    }
}
