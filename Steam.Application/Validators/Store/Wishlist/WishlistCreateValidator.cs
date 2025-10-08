using FluentValidation;
using Steam.Application.DTOs.Store.Wishlist;

namespace Steam.Application.Validators.Store.Wishlist
{
    public class WishlistCreateValidator : AbstractValidator<WishlistCreateDto>
    {
        public WishlistCreateValidator()
        {
            // FIXED: Changed validation rule for string type
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("A valid UserId is required.");

            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");
        }
    }
}
