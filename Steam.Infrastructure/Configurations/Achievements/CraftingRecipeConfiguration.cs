using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Achievements
{
    public class CraftingRecipeConfiguration : IEntityTypeConfiguration<CraftingRecipe>
    {
        public void Configure(EntityTypeBuilder<CraftingRecipe> builder)
        {
            builder.ToTable("CraftingRecipes");
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Name).IsRequired().HasMaxLength(150);

            // Relationship to the resulting badge
            builder.HasOne(cr => cr.ResultBadge)
                   .WithMany()
                   .HasForeignKey(cr => cr.ResultBadgeId)
                   .OnDelete(DeleteBehavior.Restrict); // CHANGED FROM CASCADE (DEFAULT) TO RESTRICT

            builder.HasQueryFilter(cr => !cr.IsDeleted);
        }
    }
}
