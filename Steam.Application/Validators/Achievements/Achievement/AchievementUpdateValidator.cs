using FluentValidation;
using Steam.Application.DTOs.Achievements.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Achievements.Achievement
{
    public class AchievementUpdateValidator : AbstractValidator<AchievementUpdateDto>
    {
        public AchievementUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Achievement name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Points cannot be negative.");
        }
    }
}
