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
    public class CraftingRecipeRequirementConfiguration : IEntityTypeConfiguration<CraftingRecipeRequirement>
    {
        public void Configure(EntityTypeBuilder<CraftingRecipeRequirement> builder)
        {
            builder.ToTable("CraftingRecipeRequirements");
            builder.HasKey(r => new { r.CraftingRecipeId, r.RequiredBadgeId });

            builder.HasOne(r => r.CraftingRecipe)
                   .WithMany(cr => cr.Requirements)
                   .HasForeignKey(r => r.CraftingRecipeId);

            // Relationship to Badge
            builder.HasOne(r => r.RequiredBadge)
                   .WithMany()
                   .HasForeignKey(r => r.RequiredBadgeId)
                   .OnDelete(DeleteBehavior.Restrict); // CHANGED FROM CASCADE (DEFAULT) TO RESTRICT
        }
    }
}
