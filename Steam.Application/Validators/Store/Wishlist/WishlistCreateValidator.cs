using FluentValidation;
using Steam.Application.DTOs.Store.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Store.Wishlist
{
    public class WishlistCreateValidator : AbstractValidator<WishlistCreateDto>
    {
        public WishlistCreateValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("A valid UserId is required.");

            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");
        }
    }
}
