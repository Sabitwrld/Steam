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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, ICartService cartService, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _cartService.ClearCartAsync(userId); // Bu metod özü artıq Commit etmir

            await _unitOfWork.CommitAsync(); // Bütün dəyişikliklər burada saxlanılır

            return _mapper.Map<OrderReturnDto>(order);
        }

        public async Task<OrderReturnDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetEntityAsync( // Dəyişdirildi
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

        public async Task<PagedResponse<OrderListItemDto>> GetOrdersByUserIdAsync(
            string userId, int pageNumber, int pageSize)
        {
            // Sorğu məntiqi Repository-yə daşındı
            var (items, totalCount) = await _unitOfWork.OrderRepository
                .GetByUserIdPagedAsync(userId, pageNumber, pageSize);

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
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId); // Dəyişdirildi
            if (order == null)
                throw new NotFoundException(nameof(Order), orderId);

            order.Status = status;
            _unitOfWork.OrderRepository.Update(order); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi

            return _mapper.Map<OrderReturnDto>(order);
        }

        public async Task<PagedResponse<OrderListItemForAdminDto>> GetAllOrdersForAdminAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.OrderRepository.GetAllPagedAsync(pageNumber, pageSize,
                includes: new Func<IQueryable<Order>, IQueryable<Order>>[]
                {
            q => q.Include(o => o.User),
            q => q.Include(o => o.Items)
                });

            return new PagedResponse<OrderListItemForAdminDto>
            {
                Data = _mapper.Map<List<OrderListItemForAdminDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
