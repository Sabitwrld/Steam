using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReturnDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderReturnDto> UpdateOrderAsync(int id, OrderUpdateDto dto);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderReturnDto> GetOrderByIdAsync(int id);
        Task<PagedResponse<OrderListItemDto>> GetAllOrdersAsync(int pageNumber, int pageSize);
    }
}
