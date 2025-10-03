namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftReturnDto
    {
        public int Id { get; init; }
        public int SenderId { get; init; }
        public int ReceiverId { get; init; }
        public int ApplicationId { get; init; }
        public DateTime SentAt { get; init; }
        public bool IsRedeemed { get; init; }
    }
}
