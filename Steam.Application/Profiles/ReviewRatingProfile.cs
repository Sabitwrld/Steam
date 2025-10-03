using AutoMapper;
using Steam.Application.DTOs.ReviewsRating.Rating;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Domain.Entities.ReviewsRating;

namespace Steam.Application.Profiles
{
    public class ReviewRatingProfile : Profile
    {
        public ReviewRatingProfile()
        {
            // Review
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();
            CreateMap<Review, ReviewReturnDto>();
            CreateMap<Review, ReviewListItemDto>();

            // Rating
            CreateMap<RatingCreateDto, Rating>();
            CreateMap<RatingUpdateDto, Rating>();
            CreateMap<Rating, RatingReturnDto>();
            CreateMap<Rating, RatingListItemDto>();
        }
    }
}
