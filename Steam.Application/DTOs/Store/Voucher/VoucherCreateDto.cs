namespace Steam.Application.DTOs.Store.Voucher
{
    public record VoucherCreateDto
    {
        public string Code { get; init; } = default!;
        public int ApplicationId { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
