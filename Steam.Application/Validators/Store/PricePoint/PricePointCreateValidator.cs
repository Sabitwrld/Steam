using FluentValidation;
using Steam.Application.DTOs.Store.PricePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Store.PricePoint
{
    public class PricePointCreateValidator : AbstractValidator<PricePointCreateDto>
    {
        public PricePointCreateValidator()
        {
            RuleFor(p => p.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Price point name cannot be empty.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(p => p.BasePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Base price cannot be negative.");
        }
    }
}
