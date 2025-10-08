using FluentValidation;
using Steam.Application.DTOs.Orders.Order;

namespace Steam.Application.Validators.Orders.Order
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            // FIXED: Changed validation rule from GreaterThan(0) to NotEmpty() for the string type.
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("A valid UserId is required to create an order.");
        }
    }
}
