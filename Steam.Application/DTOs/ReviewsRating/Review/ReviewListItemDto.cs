using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public string Content { get; init; } = string.Empty;
    }
}
