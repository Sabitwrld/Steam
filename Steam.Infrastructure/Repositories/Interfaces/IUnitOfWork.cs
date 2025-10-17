using Microsoft.EntityFrameworkCore.Storage;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;
using Steam.Infrastructure.Repositories.Interfaces.Library;
using Steam.Infrastructure.Repositories.Interfaces.Orders;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IApplicationCatalogRepository ApplicationCatalogRepository { get; }
        IGenreRepository GenreRepository { get; }
        IMediaRepository MediaRepository { get; }
        ISystemRequirementsRepository SystemRequirementsRepository { get; }
        ITagRepository TagRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        ICouponRepository CouponRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        IGiftRepository GiftRepository { get; }
        IPricePointRepository PricePointRepository { get; }
        IRegionalPriceRepository RegionalPriceRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IRefundRepository RefundRepository { get; }
        ILicenseRepository LicenseRepository { get; }
        IUserLibraryRepository UserLibraryRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IAchievementRepository AchievementRepository { get; }
        IBadgeRepository BadgeRepository { get; }
        ICraftingRecipeRepository CraftingRecipeRepository { get; }
        ILeaderboardRepository LeaderboardRepository { get; }
        IUserAchievementRepository UserAchievementRepository { get; }

        Task<int> CommitAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
