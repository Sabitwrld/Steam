namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftListItemDto
    {
        public int Id { get; init; }
        public string SenderId { get; init; }
        public string ReceiverId { get; init; }
        public int ApplicationId { get; init; }
        public bool IsRedeemed { get; init; }
    }
}
