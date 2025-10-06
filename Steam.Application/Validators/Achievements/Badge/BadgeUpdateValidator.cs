using FluentValidation;
using Steam.Application.DTOs.Achievements.Badge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Achievements.Badge
{
    public class BadgeUpdateValidator : AbstractValidator<BadgeUpdateDto>
    {
        public BadgeUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Badge name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
