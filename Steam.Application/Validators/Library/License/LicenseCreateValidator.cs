using FluentValidation;
using Steam.Application.DTOs.Library.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Library.License
{
    public class LicenseCreateValidator : AbstractValidator<LicenseCreateDto>
    {
        private readonly string[] _allowedTypes = { "Lifetime", "Subscription" };

        public LicenseCreateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");

            RuleFor(x => x.LicenseType)
                .NotEmpty().WithMessage("LicenseType is required.")
                .Must(type => _allowedTypes.Contains(type))
                .WithMessage($"Invalid LicenseType. Allowed types are: {string.Join(", ", _allowedTypes)}");

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("ExpirationDate must be in the future.")
                .When(x => x.LicenseType == "Subscription"); // This rule only applies if it's a subscription
        }
    }
}
