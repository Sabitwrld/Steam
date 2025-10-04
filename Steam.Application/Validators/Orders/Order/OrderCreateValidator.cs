using FluentValidation;
using Steam.Application.DTOs.Orders.Order;

namespace Steam.Application.Validators.Orders.Order
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("A valid UserId is required to create an order.");
        }
    }
}
