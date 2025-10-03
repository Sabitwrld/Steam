using FluentValidation;
using Steam.Application.DTOs.Store.Discount;

namespace Steam.Application.Validators.Store.Discount
{
    public class DiscountUpdateValidator : AbstractValidator<DiscountUpdateDto>
    {
        public DiscountUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.CampaignId)
                .GreaterThan(0).When(x => x.CampaignId.HasValue)
                .WithMessage("A valid CampaignId is required if provided.");

            RuleFor(x => x.Percentage)
                .InclusiveBetween(1, 99).WithMessage("Discount percentage must be between 1 and 99.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");
        }
    }
}
