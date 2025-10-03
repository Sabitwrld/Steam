using AutoMapper;
using Steam.Application.DTOs.Store.Campaign;
using Steam.Application.DTOs.Store.Coupon;
using Steam.Application.DTOs.Store.Discount;
using Steam.Application.DTOs.Store.Gift;
using Steam.Application.DTOs.Store.PricePoint;
using Steam.Application.DTOs.Store.RegionalPrice;
using Steam.Application.DTOs.Store.Voucher;
using Steam.Application.DTOs.Store.Wishlist;
using Steam.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            // CAMPAIGN
            CreateMap<CampaignCreateDto, Campaign>();
            CreateMap<CampaignUpdateDto, Campaign>();
            CreateMap<Campaign, CampaignReturnDto>(); // This will automatically map the Discounts collection if names match
            CreateMap<Campaign, CampaignListItemDto>();

            // COUPON
            CreateMap<CouponCreateDto, Coupon>()
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.Percentage))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpirationDate));

            CreateMap<CouponUpdateDto, Coupon>()
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.Percentage))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpirationDate));

            CreateMap<Coupon, CouponReturnDto>()
                .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.DiscountPercent))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpiryDate));

            CreateMap<Coupon, CouponListItemDto>();

            // DISCOUNT
            CreateMap<DiscountCreateDto, Discount>();
            CreateMap<DiscountUpdateDto, Discount>();
            CreateMap<Discount, DiscountReturnDto>();
            CreateMap<Discount, DiscountListItemDto>();

            // GIFT
            CreateMap<GiftCreateDto, Gift>();
            CreateMap<Gift, GiftReturnDto>();
            CreateMap<Gift, GiftListItemDto>();

            // PRICE POINT
            CreateMap<PricePointCreateDto, PricePoint>();
            CreateMap<PricePointUpdateDto, PricePoint>();
            CreateMap<PricePoint, PricePointReturnDto>(); // This will automatically map RegionalPrices
            CreateMap<PricePoint, PricePointListItemDto>();

            // REGIONAL PRICE
            CreateMap<RegionalPriceCreateDto, RegionalPrice>();
            CreateMap<RegionalPriceUpdateDto, RegionalPrice>();
            CreateMap<RegionalPrice, RegionalPriceReturnDto>();
            CreateMap<RegionalPrice, RegionalPriceListItemDto>();

            // VOUCHER
            CreateMap<VoucherCreateDto, Voucher>();
            CreateMap<VoucherUpdateDto, Voucher>();
            CreateMap<Voucher, VoucherReturnDto>();
            CreateMap<Voucher, VoucherListItemDto>();

            // WISHLIST
            CreateMap<WishlistCreateDto, Wishlist>();
            CreateMap<Wishlist, WishlistReturnDto>();
            CreateMap<Wishlist, WishlistListItemDto>();
        }
    }
}
