using FluentValidation;
using Steam.Application.DTOs.Library.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Library.License
{
    public class LicenseUpdateValidator : AbstractValidator<LicenseUpdateDto>
    {
        public LicenseUpdateValidator()
        {
            RuleFor(x => x.PlaytimeInMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("Playtime cannot be negative.")
                .When(x => x.PlaytimeInMinutes.HasValue);

            RuleFor(x => x.LastPlayed)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("LastPlayed date cannot be in the future.")
                .When(x => x.LastPlayed.HasValue);
        }
    }
}
