using FluentValidation;
using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.Validators.Orders.CartItem
{
    public class CartItemUpdateValidator : AbstractValidator<CartItemUpdateDto>
    {
        public CartItemUpdateValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .InclusiveBetween(1, 10).WithMessage("Quantity must be between 1 and 10.");
        }
    }
}
