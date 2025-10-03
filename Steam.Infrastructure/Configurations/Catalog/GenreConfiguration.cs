using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Configurations.Catalog
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(g => g.Applications)
                .WithMany(a => a.Genres);

            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}
