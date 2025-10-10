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

            // Sender (Göndərən) əlaqəsi üçün silmə məhdudlaşdırıldı
            builder.HasOne(g => g.Sender)
                   .WithMany()
                   .HasForeignKey(g => g.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Receiver (Qəbul edən) əlaqəsi üçün silmə məhdudlaşdırıldı
            builder.HasOne(g => g.Receiver)
                   .WithMany()
                   .HasForeignKey(g => g.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict); // ƏVVƏL CASCADE İDİ, RESTRICT OLDU

            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}
