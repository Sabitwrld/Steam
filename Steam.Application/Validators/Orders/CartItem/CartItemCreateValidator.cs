using FluentValidation;
using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.Validators.Orders.CartItem
{
    public class CartItemCreateValidator : AbstractValidator<CartItemCreateDto>
    {
        public CartItemCreateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty().WithMessage("ApplicationId is required.")
                .GreaterThan(0).WithMessage("A valid ApplicationId must be provided.");
        }
    }
}
