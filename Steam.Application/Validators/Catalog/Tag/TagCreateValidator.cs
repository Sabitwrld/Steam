using FluentValidation;
using Steam.Application.DTOs.Catalog.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
