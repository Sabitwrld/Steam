using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface ICartService
    {
        Task<CartReturnDto> GetCartByUserIdAsync(int userId);
        Task<CartReturnDto> AddItemToCartAsync(int userId, CartItemCreateDto dto);
        Task<CartReturnDto> UpdateItemQuantityAsync(int userId, int cartItemId, int quantity);
        Task<bool> RemoveItemFromCartAsync(int userId, int cartItemId);
        Task<bool> ClearCartAsync(int userId);
    }
}
