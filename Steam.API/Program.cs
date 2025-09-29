
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Application.Profiles;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Application.Services.Library.Implementations;
using Steam.Application.Services.Library.Interfaces;
using Steam.Application.Services.ReviewsRating.Implementations;
using Steam.Application.Services.ReviewsRating.Interfaces;
using Steam.Application.Services.Store.Implementations;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Implementations;
using Steam.Infrastructure.Repositories.Implementations.Catalog;
using Steam.Infrastructure.Repositories.Implementations.Library;
using Steam.Infrastructure.Repositories.Implementations.Orders;
using Steam.Infrastructure.Repositories.Implementations.ReviewsRating;
using Steam.Infrastructure.Repositories.Implementations.Store;
using Steam.Infrastructure.Repositories.Interfaces;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;
using Steam.Infrastructure.Repositories.Interfaces.Library;
using Steam.Infrastructure.Repositories.Interfaces.Orders;
using Steam.Infrastructure.Repositories.Interfaces.ReviewsRating;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


            #region Register Repositories
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #region Catalog Repositories
            builder.Services.AddScoped<IApplicationCatalogRepository, ApplicationCatalogRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IMediaRepository, MediaRepository>();
            builder.Services.AddScoped<ISystemRequirementsRepository, SystemRequirementsRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            #endregion
            #region Store Repositories
            builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
            builder.Services.AddScoped<ICouponRepository, CouponRepository>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddScoped<IPricePointRepository, PricePointRepository>();
            builder.Services.AddScoped<IRegionalPriceRepository, RegionalPriceRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            #endregion
            #region Orders Repositories
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IRefundRepository, RefundRepository>();
            #endregion
            #region Library Repositories
            builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
            builder.Services.AddScoped<IUserLibraryRepository, UserLibraryRepository>();
            #endregion
            #region Reviews & Rating Repositories
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            #endregion
            #endregion

            #region Register Services
            #region Catalog Services
            builder.Services.AddScoped<IApplicationCatalogService, ApplicationCatalogService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<ISystemRequirementsService, SystemRequirementsService>();
            builder.Services.AddScoped<ITagService, TagService>();
            #endregion
            #region Store Services
            builder.Services.AddScoped<ICampaignService, CampaignService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IPricePointService, PricePointService>();
            builder.Services.AddScoped<IRegionalPriceService, RegionalPriceService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            #endregion
            #region Orders Services

            #endregion
            #region Library Services
            builder.Services.AddScoped<ILicenseService, LicenseService>();
            builder.Services.AddScoped<IUserLibraryService, UserLibraryService>();
            #endregion
            #region Reviews & Rating Services
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            #endregion
            #endregion

            #region Register AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(CatalogProfile).Assembly);
            });
            
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(StoreProfile).Assembly);
            });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(OrdersProfile).Assembly);
            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(LibraryProfile).Assembly);
            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ReviewRatingProfile).Assembly);
            });
            #endregion



            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders(); ;

            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerFileOperationFilter>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()   // test üçün
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
