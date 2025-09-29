using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.ReviewsRating.Rating
{
    public record RatingListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public int Score { get; init; }
    }

}
