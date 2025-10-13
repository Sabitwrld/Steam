using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Implementations.Achievements;
using Steam.Infrastructure.Repositories.Implementations.Catalog;
using Steam.Infrastructure.Repositories.Implementations.Library;
using Steam.Infrastructure.Repositories.Implementations.Orders;
using Steam.Infrastructure.Repositories.Implementations.ReviewsRating;
using Steam.Infrastructure.Repositories.Implementations.Store;
using Steam.Infrastructure.Repositories.Interfaces;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;
using Steam.Infrastructure.Repositories.Interfaces.Library;
using Steam.Infrastructure.Repositories.Interfaces.Orders;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;
using Steam.Infrastructure.Repositories.Interfaces.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IApplicationCatalogRepository ApplicationCatalogRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public IMediaRepository MediaRepository { get; private set; }
        public ISystemRequirementsRepository SystemRequirementsRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public ICampaignRepository CampaignRepository { get; private set; }
        public ICouponRepository CouponRepository { get; private set; }
        public IDiscountRepository DiscountRepository { get; private set; }
        public IGiftRepository GiftRepository { get; private set; }
        public IPricePointRepository PricePointRepository { get; private set; }
        public IRegionalPriceRepository RegionalPriceRepository { get; private set; }
        public IVoucherRepository VoucherRepository { get; private set; }
        public IWishlistRepository WishlistRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }
        public ICartItemRepository CartItemRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderItemRepository OrderItemRepository { get; private set; }
        public IPaymentRepository PaymentRepository { get; private set; }
        public IRefundRepository RefundRepository { get; private set; }
        public ILicenseRepository LicenseRepository { get; private set; }
        public IUserLibraryRepository UserLibraryRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public IAchievementRepository AchievementRepository { get; private set; }
        public IBadgeRepository BadgeRepository { get; private set; }
        public ICraftingRecipeRepository CraftingRecipeRepository { get; private set; }
        public ILeaderboardRepository LeaderboardRepository { get; private set; }
        public IUserAchievementRepository UserAchievementRepository { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            ApplicationCatalogRepository = new ApplicationCatalogRepository(_context);
            GenreRepository = new GenreRepository(_context);
            MediaRepository = new MediaRepository(_context);
            SystemRequirementsRepository = new SystemRequirementsRepository(_context);
            TagRepository = new TagRepository(_context);
            CampaignRepository = new CampaignRepository(_context);
            CouponRepository = new CouponRepository(_context);
            DiscountRepository = new DiscountRepository(_context);
            GiftRepository = new GiftRepository(_context);
            PricePointRepository = new PricePointRepository(_context);
            RegionalPriceRepository = new RegionalPriceRepository(_context);
            VoucherRepository = new VoucherRepository(_context);
            WishlistRepository = new WishlistRepository(_context);
            CartRepository = new CartRepository(_context);
            CartItemRepository = new CartItemRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderItemRepository = new OrderItemRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            RefundRepository = new RefundRepository(_context);
            LicenseRepository = new LicenseRepository(_context);
            UserLibraryRepository = new UserLibraryRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            AchievementRepository = new AchievementRepository(_context);
            BadgeRepository = new BadgeRepository(_context);
            CraftingRecipeRepository = new CraftingRecipeRepository(_context);
            LeaderboardRepository = new LeaderboardRepository(_context);
            UserAchievementRepository = new UserAchievementRepository(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
