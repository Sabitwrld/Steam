namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistCreateDto
    {
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
    }
}
