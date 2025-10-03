namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
    }
}
