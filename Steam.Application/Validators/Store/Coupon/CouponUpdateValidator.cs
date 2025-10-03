using FluentValidation;
using Steam.Application.DTOs.Store.Coupon;

namespace Steam.Application.Validators.Store.Coupon
{
    public class CouponUpdateValidator : AbstractValidator<CouponUpdateDto>
    {
        public CouponUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Code).NotEmpty().Length(6, 20);
            RuleFor(x => x.Percentage).InclusiveBetween(1, 100);
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.UtcNow);
        }
    }
}
