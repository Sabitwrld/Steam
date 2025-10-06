using FluentValidation;
using Steam.Application.DTOs.Achievements.CraftingRecipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Achievements.CraftingRecipe
{
    public class CraftingRecipeCreateValidator : AbstractValidator<CraftingRecipeCreateDto>
    {
        public CraftingRecipeCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Recipe name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(x => x.ResultBadgeId)
                .GreaterThan(0).WithMessage("A valid ResultBadgeId is required.");

            RuleFor(x => x.RequiredBadgeIds)
                .NotEmpty().WithMessage("At least one required badge is necessary.")
                .ForEach(id => id.GreaterThan(0)).WithMessage("All badge IDs must be valid.");
        }
    }
}
