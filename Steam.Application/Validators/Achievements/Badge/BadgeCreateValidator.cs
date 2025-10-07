using FluentValidation;
using Steam.Application.DTOs.Achievements.Badge;

namespace Steam.Application.Validators.Achievements.Badge
{
    public class BadgeCreateValidator : AbstractValidator<BadgeCreateDto>
    {
        public BadgeCreateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Badge name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");
        }
    }
}
