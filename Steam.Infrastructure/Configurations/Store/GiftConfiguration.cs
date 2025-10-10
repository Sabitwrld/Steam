using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Configurations.Store
{
    public class GiftConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.ToTable("Gifts");
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Application)
                   .WithMany()
                   .HasForeignKey(g => g.ApplicationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Sender)
                   .WithMany()
                   .HasForeignKey(g => g.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Receiver)
                   .WithMany()
                   .HasForeignKey(g => g.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict); // DƏYİŞDİRİLDİ

            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}
