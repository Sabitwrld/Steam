using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                   .HasForeignKey(g => g.ApplicationId);

            // Note: Relationships for SenderId and ReceiverId to AppUser are configured by Identity implicitly

            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}
