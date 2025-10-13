using FluentValidation;
using Steam.Application.DTOs.Library.License;
using Steam.Domain.Constants;

namespace Steam.Application.Validators.Library.License
{
    public class LicenseCreateValidator : AbstractValidator<LicenseCreateDto>
    {
        private readonly string[] _allowedTypes = { LicenseTypes.Lifetime, LicenseTypes.Subscription }; // DƏYİŞDİRİLDİ

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
                .When(x => x.LicenseType == LicenseTypes.Subscription); // DƏYİŞDİRİLDİ
        }
    }
}
