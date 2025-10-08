namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistReturnDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public int ApplicationId { get; init; }
        public DateTime AddedDate { get; init; }
    }

}
