namespace Steam.Application.DTOs.Store.Voucher
{
    public record VoucherUpdateDto
    {
        public int Id { get; init; }
        public bool IsUsed { get; init; }
    }
}
