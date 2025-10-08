namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistCreateDto
    {
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public int ApplicationId { get; init; }
    }
}
