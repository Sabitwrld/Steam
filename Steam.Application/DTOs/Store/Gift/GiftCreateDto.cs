namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftCreateDto
    {
        public string SenderId { get; init; }
        public string ReceiverId { get; init; }
        public int ApplicationId { get; init; }
    }
}
