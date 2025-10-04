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

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .InclusiveBetween(1, 10).WithMessage("Quantity must be between 1 and 10.");
        }
    }
}
