using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReturnDto> CreateOrderFromCartAsync(int userId);
        Task<OrderReturnDto> GetOrderByIdAsync(int orderId);
        Task<PagedResponse<OrderListItemDto>> GetOrdersForUserAsync(int userId, int pageNumber, int pageSize);
        Task<OrderReturnDto> UpdateOrderStatusAsync(int orderId, string status);
    }
}
