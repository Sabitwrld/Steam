namespace Steam.Application.DTOs.Store.Voucher
{
    public record VoucherReturnDto
    {
        public int Id { get; init; }
        public string Code { get; init; } = default!;
        public int ApplicationId { get; init; }
        public DateTime ExpirationDate { get; init; }
        public bool IsUsed { get; init; }
    }
}
