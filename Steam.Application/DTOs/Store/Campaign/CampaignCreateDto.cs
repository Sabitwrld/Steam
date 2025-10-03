namespace Steam.Application.DTOs.Store.Campaign
{
    public record CampaignCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
