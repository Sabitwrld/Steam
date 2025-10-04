using AutoMapper;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Domain.Entities.ReviewsRating;

namespace Steam.Application.Profiles
{
    public class ReviewRatingProfile : Profile
    {
        public ReviewRatingProfile()
        {
            // Review Mappings
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();

            CreateMap<Review, ReviewReturnDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Review, ReviewListItemDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))

                // --- ADD THIS LOGIC HERE ---
                .ForMember(dest => dest.ContentShort, opt => opt.MapFrom(src =>
                    src.Content.Length > 200
                        ? src.Content.Substring(0, 200) + "..."
                        : src.Content));
        }
    }
}
