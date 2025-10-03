using FluentValidation;
using Steam.Application.DTOs.Store.Coupon;

namespace Steam.Application.Validators.Store.Coupon
{
    public class CouponCreateValidator : AbstractValidator<CouponCreateDto>
    {
        public CouponCreateValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Coupon code is required.")
                .Length(6, 20).WithMessage("Coupon code must be between 6 and 20 characters.");

            RuleFor(x => x.Percentage)
                .InclusiveBetween(1, 100).WithMessage("Percentage must be between 1 and 100.");

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        }
    }
}
