using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface ICartService
    {
        Task<CartReturnDto> GetCartByUserIdAsync(int userId);
        Task<CartReturnDto> AddItemAsync(int userId, CartItemCreateDto dto);
        Task<CartReturnDto> UpdateItemQuantityAsync(int userId, int itemId, int quantity);
        Task<bool> RemoveItemAsync(int userId, int itemId);
        Task<bool> ClearCartAsync(int userId);
    }
}
