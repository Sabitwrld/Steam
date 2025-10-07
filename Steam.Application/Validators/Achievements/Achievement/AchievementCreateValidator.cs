using FluentValidation;
using Steam.Application.DTOs.Achievements.Achievements;

namespace Steam.Application.Validators.Achievements.Achievement
{
    public class AchievementCreateValidator : AbstractValidator<AchievementCreateDto>
    {
        public AchievementCreateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");

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
