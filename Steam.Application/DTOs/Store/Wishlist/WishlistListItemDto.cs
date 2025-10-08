namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistListItemDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public int ApplicationId { get; init; }
    }
}
