using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Orders.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepo, ICartService cartService, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task<OrderReturnDto> CreateOrderFromCartAsync(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart.Items == null || !cart.Items.Any())
            {
                throw new Exception("Cannot create an order from an empty cart.");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.TotalPrice,
                Status = "Pending"
            };

            order.Items = cart.Items.Select(cartItem => new OrderItem
            {
                ApplicationId = cartItem.ApplicationId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Price
            }).ToList();

            await _orderRepo.CreateAsync(order);
            await _cartService.ClearCartAsync(userId);

            return _mapper.Map<OrderReturnDto>(order);
        }

        public async Task<OrderReturnDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepo.GetEntityAsync(
                predicate: o => o.Id == orderId,
                includes: new[] {
                    (Func<IQueryable<Order>, IQueryable<Order>>)(q => q.Include(o => o.Items).ThenInclude(oi => oi.Application)),
                    (Func<IQueryable<Order>, IQueryable<Order>>)(q => q.Include(o => o.Payment))
                }
            );

            if (order == null)
                throw new NotFoundException(nameof(Order), orderId);

            return _mapper.Map<OrderReturnDto>(order);
        }

        // FIXED: Renamed this method from GetOrdersForUserAsync to GetOrdersByUserIdAsync
        public async Task<PagedResponse<OrderListItemDto>> GetOrdersByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _orderRepo.GetQuery(o => o.UserId == userId, asNoTracking: true)
                                   .Include(o => o.Items); // Include items to get the count

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(o => o.OrderDate)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            return new PagedResponse<OrderListItemDto>
            {
                Data = _mapper.Map<List<OrderListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<OrderReturnDto> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException(nameof(Order), orderId);

            order.Status = status;
            await _orderRepo.UpdateAsync(order);
            return _mapper.Map<OrderReturnDto>(order);
        }
    }
}
