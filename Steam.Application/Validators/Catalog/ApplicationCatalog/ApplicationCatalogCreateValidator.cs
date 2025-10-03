using FluentValidation;
using Steam.Application.DTOs.Catalog.Application;

namespace Steam.Application.Validators.Catalog.ApplicationCatalog
{
    public class ApplicationCatalogCreateValidator : AbstractValidator<ApplicationCatalogCreateDto>
    {
        public ApplicationCatalogCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The game name cannot be empty")
                .MaximumLength(200).WithMessage("The game name cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The description cannot be empty");

            RuleFor(x => x.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Release date cannot be in the future");

            RuleFor(x => x.Developer)
                .NotEmpty().WithMessage("The developer name cannot be empty");

            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("The publisher name cannot be empty");

            RuleFor(x => x.ApplicationType)
                .NotEmpty().WithMessage("The application type cannot be empty");
        }
    }

}
