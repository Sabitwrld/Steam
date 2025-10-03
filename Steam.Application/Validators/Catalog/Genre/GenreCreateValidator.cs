using FluentValidation;
using Steam.Application.DTOs.Catalog.Genre;

namespace Steam.Application.Validators.Catalog.Genre
{
    public class GenreCreateValidator : AbstractValidator<GenreCreateDto>
    {
        public GenreCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name cannot be empty")
                .MaximumLength(100).WithMessage("Genre name cannot exceed 100 characters");
        }
    }
}
