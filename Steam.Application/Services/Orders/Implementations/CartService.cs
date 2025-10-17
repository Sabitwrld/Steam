using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.Exceptions;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Domain.Entities.Orders;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Orders.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartReturnDto> GetCartByUserIdAsync(string userId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartDto = _mapper.Map<CartReturnDto>(cart);
            cartDto.TotalPrice = 0;

            var itemDtos = new List<CartItemReturnDto>();
            if (cart.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    var itemDto = _mapper.Map<CartItemReturnDto>(item);
                    itemDto.Price = await CalculateItemPrice(item.ApplicationId);
                    itemDtos.Add(itemDto);
                    cartDto.TotalPrice += itemDto.TotalPrice;
                }
            }
            cartDto.Items = itemDtos;

            return cartDto;
        }

        public async Task<CartReturnDto> AddItemToCartAsync(string userId, CartItemCreateDto dto)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var existingItem = cart.Items?.FirstOrDefault(i => i.ApplicationId == dto.ApplicationId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity > 0 ? dto.Quantity : 1;
                _unitOfWork.CartItemRepository.Update(existingItem); // Dəyişdirildi
            }
            else
            {
                var newCartItem = _mapper.Map<CartItem>(dto);
                newCartItem.CartId = cart.Id;
                await _unitOfWork.CartItemRepository.CreateAsync(newCartItem); // Dəyişdirildi
            }

            await _unitOfWork.CommitAsync(); // Dəyişdirildi
            return await GetCartByUserIdAsync(userId);
        }

        public async Task<CartReturnDto> UpdateItemQuantityAsync(string userId, int cartItemId, int quantity)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartItem = cart.Items?.FirstOrDefault(i => i.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(nameof(CartItem), cartItemId);

            if (quantity <= 0)
            {
                _unitOfWork.CartItemRepository.Delete(cartItem); // Dəyişdirildi
            }
            else
            {
                cartItem.Quantity = quantity;
                _unitOfWork.CartItemRepository.Update(cartItem); // Dəyişdirildi
            }

            await _unitOfWork.CommitAsync(); // Dəyişdirildi
            return await GetCartByUserIdAsync(userId);
        }

        public async Task<bool> RemoveItemFromCartAsync(string userId, int cartItemId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            var cartItem = cart.Items?.FirstOrDefault(i => i.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(nameof(CartItem), cartItemId);

            _unitOfWork.CartItemRepository.Delete(cartItem); // Dəyişdirildi
            await _unitOfWork.CommitAsync(); // Dəyişdirildi
            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await GetOrCreateCartByUserId(userId);
            if (cart.Items == null || !cart.Items.Any()) return true;

            foreach (var item in cart.Items.ToList())
            {
                _unitOfWork.CartItemRepository.Delete(item); // Dəyişdirildi
            }

            // CommitAsync burada çağırılmır, çünki OrderService tərəfindən idarə olunacaq
            // await _unitOfWork.CommitAsync();

            return true;
        }

        private async Task<Cart> GetOrCreateCartByUserId(string userId)
        {
            // Addım 1: Repository-dəki xüsusi metodu çağırın
            var cart = await _unitOfWork.CartRepository.GetByUserIdWithItemsAsync(userId);

            // Addım 2: Əgər səbət yoxdursa, yenisini yaradın
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _unitOfWork.CartRepository.CreateAsync(cart);
                await _unitOfWork.CommitAsync(); // Yeni səbət yaradıldığı üçün dərhal yadda saxlanılmalıdır
            }
            return cart;
        }

        private async Task<decimal> CalculateItemPrice(int applicationId)
        {
            var pricePoint = await _unitOfWork.PricePointRepository.GetEntityAsync(p => p.ApplicationId == applicationId); // Dəyişdirildi
            if (pricePoint == null) return 0;

            var price = pricePoint.BasePrice;

            var activeDiscount = await _unitOfWork.DiscountRepository.GetEntityAsync(d => // Dəyişdirildi
                d.ApplicationId == applicationId &&
                d.StartDate <= DateTime.UtcNow &&
                d.EndDate >= DateTime.UtcNow);

            if (activeDiscount != null)
            {
                price *= (1 - (activeDiscount.Percent / 100));
            }

            return price;
        }
    }

}
