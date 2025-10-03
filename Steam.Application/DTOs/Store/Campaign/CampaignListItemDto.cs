namespace Steam.Application.DTOs.Store.Campaign
{
    public record CampaignListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
