using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.Exceptions;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Entities.Orders;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Orders.Implementations
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartItem> _cartItemRepo;
        private readonly IRepository<PricePoint> _pricePointRepo;
        private readonly IRepository<Discount> _discountRepo;
        private readonly IMapper _mapper;

        public CartService(
            IRepository<Cart> cartRepo,
            IRepository<CartItem> cartItemRepo,
            IRepository<PricePoint> pricePointRepo,
            IRepository<Discount> discountRepo,
            IMapper mapper)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _pricePointRepo = pricePointRepo;
            _discountRepo = discountRepo;
            _mapper = mapper;
        }

        public async Task<CartReturnDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartDto = _mapper.Map<CartReturnDto>(cart);
            cartDto.TotalPrice = 0;

            var itemDtos = new List<CartItemReturnDto>();
            foreach (var item in cart.Items)
            {
                var itemDto = _mapper.Map<CartItemReturnDto>(item);
                itemDto.Price = await CalculateItemPrice(item.ApplicationId);
                itemDtos.Add(itemDto);
                cartDto.TotalPrice += itemDto.TotalPrice;
            }
            cartDto.Items = itemDtos;

            return cartDto;
        }

        public async Task<CartReturnDto> AddItemToCartAsync(int userId, CartItemCreateDto dto)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.ApplicationId == dto.ApplicationId);

            if (existingItem != null)
            {
                existingItem.Quantity = dto.Quantity > 0 ? existingItem.Quantity + dto.Quantity : existingItem.Quantity + 1;
                await _cartItemRepo.UpdateAsync(existingItem);
            }
            else
            {
                var newCartItem = _mapper.Map<CartItem>(dto);
                newCartItem.CartId = cart.Id;
                await _cartItemRepo.CreateAsync(newCartItem);
            }

            return await GetCartByUserIdAsync(userId);
        }

        public async Task<CartReturnDto> UpdateItemQuantityAsync(int userId, int cartItemId, int quantity)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(nameof(CartItem), cartItemId);

            if (quantity <= 0)
            {
                await _cartItemRepo.DeleteAsync(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                await _cartItemRepo.UpdateAsync(cartItem);
            }

            return await GetCartByUserIdAsync(userId);
        }

        public async Task<bool> RemoveItemFromCartAsync(int userId, int cartItemId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(nameof(CartItem), cartItemId);

            return await _cartItemRepo.DeleteAsync(cartItem);
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            if (!cart.Items.Any()) return true;

            _cartItemRepo.GetQuery(ci => ci.CartId == cart.Id).ToList().ForEach(async item => await _cartItemRepo.DeleteAsync(item, saveChanges: false));
            await _cartItemRepo.SaveChangesAsync();
            return true;
        }

        private async Task<Cart> GetOrCreateCartByUserId(int userId)
        {
            var cart = await _cartRepo.GetEntityAsync(
                predicate: c => c.UserId == userId,
                includes: new[] {
                    (System.Func<IQueryable<Cart>, IQueryable<Cart>>)(q => q.Include(c => c.Items)
                                                                           .ThenInclude(ci => ci.Application))
                }
            );

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepo.CreateAsync(cart);
            }
            return cart;
        }

        private async Task<decimal> CalculateItemPrice(int applicationId)
        {
            var pricePoint = await _pricePointRepo.GetEntityAsync(p => p.ApplicationId == applicationId);
            if (pricePoint == null) return 0;

            var price = pricePoint.BasePrice;

            var activeDiscount = await _discountRepo.GetEntityAsync(d =>
                d.ApplicationId == applicationId &&
                d.StartDate <= System.DateTime.UtcNow &&
                d.EndDate >= System.DateTime.UtcNow);

            if (activeDiscount != null)
            {
                price = price * (1 - (activeDiscount.Percent / 100));
            }

            return price;
        }
    }

}
