using AutoMapper;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Orders.OrderItem;
using Steam.Application.DTOs.Orders.Payment;
using Steam.Application.DTOs.Orders.Refund;
using Steam.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            // Cart
            CreateMap<Cart, CartReturnDto>();
            CreateMap<Cart, CartListItemDto>().ForMember(d => d.ItemCount, opt => opt.MapFrom(s => s.Items.Count));
            CreateMap<CartCreateDto, Cart>();
            CreateMap<CartUpdateDto, Cart>();
            //CartItem 
            CreateMap<CartItem, CartItemReturnDto>();
            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItemUpdateDto, CartItem>();
            //Order 
            CreateMap<Order, OrderReturnDto>();
            CreateMap<Order, OrderListItemDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
            //OrderItem 
            CreateMap<OrderItem, OrderItemReturnDto>();
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItemUpdateDto, OrderItem>();
            //Payment 
            CreateMap<Payment, PaymentReturnDto>();
            CreateMap<Payment, PaymentListItemDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();
            //Refund 
            CreateMap<Refund, RefundReturnDto>();
            CreateMap<Refund, RefundListItemDto>();
            CreateMap<RefundCreateDto, Refund>();
            CreateMap<RefundUpdateDto, Refund>();
        }
    }
}
