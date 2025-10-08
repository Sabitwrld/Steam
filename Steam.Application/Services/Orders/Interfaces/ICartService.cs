using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface ICartService
    {
        Task<CartReturnDto> GetCartByUserIdAsync(string userId);
        Task<CartReturnDto> AddItemToCartAsync(string userId, CartItemCreateDto dto);
        Task<CartReturnDto> UpdateItemQuantityAsync(string userId, int cartItemId, int quantity);
        Task<bool> RemoveItemFromCartAsync(string userId, int cartItemId);
        Task<bool> ClearCartAsync(string userId);
    }
}
