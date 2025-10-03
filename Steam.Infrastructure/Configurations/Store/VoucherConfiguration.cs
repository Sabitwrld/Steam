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
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(v => v.Code).IsUnique(); // Ensure voucher codes are unique

            builder.HasOne(v => v.Application)
                   .WithMany()
                   .HasForeignKey(v => v.ApplicationId);

            builder.HasQueryFilter(v => !v.IsDeleted);
        }
    }
}
