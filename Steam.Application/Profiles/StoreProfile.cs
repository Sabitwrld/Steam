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
            // 📌 PRICE POINT 
            CreateMap<PricePoint, PricePointReturnDto>();
            CreateMap<PricePoint, PricePointListItemDto>();
            CreateMap<PricePointCreateDto, PricePoint>();
            CreateMap<PricePointUpdateDto, PricePoint>();
            //📌 REGIONAL PRICE 
            CreateMap<RegionalPrice, RegionalPriceReturnDto>();
            CreateMap<RegionalPrice, RegionalPriceListItemDto>();
            CreateMap<RegionalPriceCreateDto, RegionalPrice>();
            CreateMap<RegionalPriceUpdateDto, RegionalPrice>();
            //📌 DISCOUNT 
            CreateMap<Discount, DiscountReturnDto>();
            CreateMap<Discount, DiscountListItemDto>();
            CreateMap<DiscountCreateDto, Discount>();
            CreateMap<DiscountUpdateDto, Discount>();
            //📌 COUPON 
            CreateMap<Coupon, CouponReturnDto>();
            CreateMap<Coupon, CouponListItemDto>();
            CreateMap<CouponCreateDto, Coupon>();
            CreateMap<CouponUpdateDto, Coupon>();
            //📌 CAMPAIGN 
            CreateMap<Campaign, CampaignReturnDto>();
            CreateMap<Campaign, CampaignListItemDto>();
            CreateMap<CampaignCreateDto, Campaign>();
            CreateMap<CampaignUpdateDto, Campaign>();
            //📌 WISHLIST 
            CreateMap<Wishlist, WishlistReturnDto>();
            CreateMap<Wishlist, WishlistListItemDto>();
            CreateMap<WishlistCreateDto, Wishlist>();
            // 📌 GIFT 
            CreateMap<Gift, GiftReturnDto>();
            CreateMap<Gift, GiftListItemDto>();
            CreateMap<GiftCreateDto, Gift>();
            //📌 VOUCHER 
            CreateMap<Voucher, VoucherReturnDto>();
            CreateMap<Voucher, VoucherListItemDto>();
            CreateMap<VoucherCreateDto, Voucher>();
            CreateMap<VoucherUpdateDto, Voucher>();
        }
    }
}
