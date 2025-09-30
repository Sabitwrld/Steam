using FluentValidation;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Catalog.SystemRequirements
{
    public class SystemRequirementsUpdateValidator : AbstractValidator<SystemRequirementsUpdateDto>
    {
        public SystemRequirementsUpdateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("ApplicationId must be greater than zero");

            RuleFor(x => x.RequirementType)
                .NotEmpty().WithMessage("Requirement type cannot be empty");

            RuleFor(x => x.OS)
                .NotEmpty().WithMessage("OS cannot be empty");

            RuleFor(x => x.CPU)
                .NotEmpty().WithMessage("CPU cannot be empty");

            RuleFor(x => x.GPU)
                .NotEmpty().WithMessage("GPU cannot be empty");

            RuleFor(x => x.RAM)
                .NotEmpty().WithMessage("RAM cannot be empty");

            RuleFor(x => x.Storage)
                .NotEmpty().WithMessage("Storage cannot be empty");
        }
    }
}
