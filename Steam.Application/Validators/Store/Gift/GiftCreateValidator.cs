using FluentValidation;
using Steam.Application.DTOs.Store.Gift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Validators.Store.Gift
{
    public class GiftCreateValidator : AbstractValidator<GiftCreateDto>
    {
        public GiftCreateValidator()
        {
            RuleFor(x => x.SenderId)
                .GreaterThan(0).WithMessage("A valid SenderId is required.");

            RuleFor(x => x.ReceiverId)
                .GreaterThan(0).WithMessage("A valid ReceiverId is required.");

            RuleFor(x => x)
                .Must(x => x.SenderId != x.ReceiverId)
                .WithMessage("Sender and Receiver cannot be the same person.");

            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");
        }
    }
}
