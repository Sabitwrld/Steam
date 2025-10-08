using FluentValidation;
using Steam.Application.DTOs.Library.UserLibrary;

namespace Steam.Application.Validators.Library.UserLibrary
{
    public class UserLibraryCreateValidator : AbstractValidator<UserLibraryCreateDto>
    {
        public UserLibraryCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
