using FluentValidation;
using Steam.Application.DTOs.Store.Campaign;

namespace Steam.Application.Validators.Store.Campaign
{
    public class CampaignCreateValidator : AbstractValidator<CampaignCreateDto>
    {
        public CampaignCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campaign name is required.")
                .MaximumLength(150).WithMessage("Campaign name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after the start date.");
        }
    }
}
