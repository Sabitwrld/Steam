using FluentValidation;
using Steam.Application.DTOs.Catalog.Tag;

namespace Steam.Application.Validators.Catalog.Tag
{
    public class TagCreateValidator : AbstractValidator<TagCreateDto>
    {
        public TagCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name cannot be empty")
                .MaximumLength(100).WithMessage("Tag name cannot exceed 100 characters");
        }
    }
}
