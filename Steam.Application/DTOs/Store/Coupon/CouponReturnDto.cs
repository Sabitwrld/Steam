namespace Steam.Application.DTOs.Store.Coupon
{
    public record CouponReturnDto
    {
        public int Id { get; init; }
        public string Code { get; init; } = default!;
        public double Percentage { get; init; }
        public DateTime ExpirationDate { get; init; }
        public bool IsActive { get; init; }
    }
}
