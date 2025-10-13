using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Configurations.Achievements
{
    // Steam.Infrastructure/Configurations/Achievements/CraftingRecipeRequirementConfiguration.cs

    public class CraftingRecipeRequirementConfiguration : IEntityTypeConfiguration<CraftingRecipeRequirement>
    {
        public void Configure(EntityTypeBuilder<CraftingRecipeRequirement> builder)
        {
            builder.ToTable("CraftingRecipeRequirements");
            builder.HasKey(r => new { r.CraftingRecipeId, r.RequiredBadgeId });

            builder.HasOne(r => r.CraftingRecipe)
                    .WithMany(cr => cr.Requirements)
                    .HasForeignKey(r => r.CraftingRecipeId);

            builder.HasOne(r => r.RequiredBadge)
                    .WithMany()
                    .HasForeignKey(r => r.RequiredBadgeId)
                    .OnDelete(DeleteBehavior.Restrict);

            // ADD THIS LINE TO FIX THE WARNING
            builder.HasQueryFilter(r => !r.CraftingRecipe.IsDeleted);
        }
    }
}
