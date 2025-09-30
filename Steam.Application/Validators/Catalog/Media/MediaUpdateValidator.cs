using FluentValidation;
using Steam.Application.DTOs.Catalog.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Catalog.Media
{
    public class MediaUpdateValidator : AbstractValidator<MediaUpdateDto>
    {
        public MediaUpdateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("ApplicationId must be greater than zero");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Media URL cannot be empty");

            RuleFor(x => x.MediaType)
                .NotEmpty().WithMessage("Media type cannot be empty");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or greater");
        }
    }
}
