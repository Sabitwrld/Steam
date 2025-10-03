using Steam.Application.DTOs.Store.Discount;

namespace Steam.Application.DTOs.Store.Campaign
{
    public record CampaignReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public List<DiscountListItemDto> Discounts { get; init; } = new();
    }
}
