using FluentValidation;
using Steam.Application.DTOs.Store.RegionalPrice;

namespace Steam.Application.Validators.Store.RegionalPrice
{
    public class RegionalPriceCreateValidator : AbstractValidator<RegionalPriceCreateDto>
    {
        public RegionalPriceCreateValidator()
        {
            RuleFor(r => r.PricePointId)
                .GreaterThan(0).WithMessage("A valid PricePointId is required.");

            RuleFor(r => r.Currency)
                .NotEmpty().WithMessage("Currency code cannot be empty.")
                .Length(3).WithMessage("Currency code must be 3 characters long (e.g., AZN, USD).");

            RuleFor(r => r.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative.");
        }
    }
}
