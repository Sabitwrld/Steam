using FluentValidation;
using Steam.Application.DTOs.Store.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Store.Campaign
{
    public class CampaignUpdateValidator : AbstractValidator<CampaignUpdateDto>
    {
        public CampaignUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

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
