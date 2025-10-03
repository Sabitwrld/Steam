using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Discount
{
    public record DiscountReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public int? CampaignId { get; init; }
        public decimal Percentage { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
