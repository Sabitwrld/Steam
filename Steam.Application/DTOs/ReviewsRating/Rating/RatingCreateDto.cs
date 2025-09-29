using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.ReviewsRating.Rating
{
    public record RatingCreateDto
    {
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public int Score { get; init; } // 1-10
    }
}
