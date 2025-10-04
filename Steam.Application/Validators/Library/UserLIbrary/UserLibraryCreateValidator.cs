using FluentValidation;
using Steam.Application.DTOs.Library.UserLibrary;

namespace Steam.Application.Validators.Library.UserLIbrary
{
    public class UserLibraryCreateValidator : AbstractValidator<UserLibraryCreateDto>
    {
        public UserLibraryCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("A valid UserId must be provided.");
        }
    }
}
