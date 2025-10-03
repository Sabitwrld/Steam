using FluentValidation;
using Steam.Application.DTOs.Orders.Refund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Orders.Refund
{
    public class RefundCreateValidator : AbstractValidator<RefundCreateDto>
    {
        public RefundCreateValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("PaymentId is required.")
                .GreaterThan(0).WithMessage("A valid PaymentId is required.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("A reason for the refund request is required.")
                .MinimumLength(10).WithMessage("Reason must be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("Reason text cannot exceed 1000 characters.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Refund amount must be greater than zero.");
        }
    }
}
