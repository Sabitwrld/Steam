using FluentValidation;
using Steam.Application.DTOs.ReviewsRating.Review;

namespace Steam.Application.Validators.ReviewsRating.Review
{
    public class ReviewUpdateValidator : AbstractValidator<ReviewUpdateDto>
    {
        public ReviewUpdateValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Review content cannot be empty.")
                .MinimumLength(50).WithMessage("Review content must be at least 50 characters long.")
                .MaximumLength(8000).WithMessage("Review content cannot exceed 8000 characters.");
        }
    }
}
