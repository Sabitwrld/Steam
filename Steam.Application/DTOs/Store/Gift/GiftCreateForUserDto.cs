namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftCreateForUserDto
    {
        public string ReceiverUsername { get; init; } = default!;
        public int ApplicationId { get; init; }
    }
}
