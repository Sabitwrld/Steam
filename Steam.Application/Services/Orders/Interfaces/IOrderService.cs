using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReturnDto> CreateOrderFromCartAsync(string userId); // CHANGED
        Task<OrderReturnDto> GetOrderByIdAsync(int orderId);
        Task<PagedResponse<OrderListItemDto>> GetOrdersByUserIdAsync(string userId, int pageNumber, int pageSize); // CHANGED
        Task<OrderReturnDto> UpdateOrderStatusAsync(int orderId, string status);
    }
}
