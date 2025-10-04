using FluentValidation;
using Steam.Application.DTOs.Library.UserLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
