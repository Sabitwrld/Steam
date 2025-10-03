using AutoMapper;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Orders.OrderItem;
using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Orders.Refund;
using Steam.Domain.Entities.Orders;

namespace Steam.Application.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            // Cart & CartItem Mappings
            CreateMap<Cart, CartReturnDto>(); // Note: TotalPrice will be calculated in the service layer.

            CreateMap<CartItem, CartItemReturnDto>()
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name));
            // Note: Price is not on CartItem entity. It must be fetched and mapped in the service.

            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItemUpdateDto, CartItem>();


            // Order & OrderItem Mappings
            CreateMap<OrderCreateDto, Order>();

            CreateMap<Order, OrderReturnDto>();

            CreateMap<Order, OrderListItemDto>()
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.Items.Count));

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name));


            // Payment Mappings
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<Payment, PaymentReturnDto>();


            // Refund Mappings
            CreateMap<RefundCreateDto, Refund>();
            CreateMap<Refund, RefundReturnDto>();
        }
    }
}
