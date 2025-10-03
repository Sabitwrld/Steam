using FluentValidation;
using Steam.Application.DTOs.Orders.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Orders.Payment
{
    public class PaymentCreateValidator : AbstractValidator<PaymentCreateDto>
    {
        private readonly string[] _allowedMethods = { "Card", "PayPal", "Wallet" };

        public PaymentCreateValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.")
                .GreaterThan(0).WithMessage("A valid OrderId is required.");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Payment method cannot be empty.")
                .MaximumLength(50).WithMessage("Payment method name is too long.")
                .Must(method => _allowedMethods.Contains(method))
                .WithMessage($"Invalid payment method. Allowed methods are: {string.Join(", ", _allowedMethods)}");
        }
    }
}
