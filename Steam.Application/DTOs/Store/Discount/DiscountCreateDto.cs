namespace Steam.Application.DTOs.Store.Discount
{
    public record DiscountCreateDto
    {
        public int ApplicationId { get; init; }
        public int? CampaignId { get; init; }
        public decimal Percentage { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
