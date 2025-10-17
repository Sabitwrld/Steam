
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Steam.API.Middlewares;
using Steam.Application.Profiles;
using Steam.Application.Services;
using Steam.Application.Services.Achievements.Implementations;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Application.Services.Auth.Implementations;
using Steam.Application.Services.Auth.Interfaces;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Application.Services.Library.Implementations;
using Steam.Application.Services.Library.Interfaces;
using Steam.Application.Services.Orders.Implementations;
using Steam.Application.Services.Orders.Interfaces;
using Steam.Application.Services.ReviewsRating.Implementations;
using Steam.Application.Services.ReviewsRating.Interfaces;
using Steam.Application.Services.Store.Implementations;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Identity;
using Steam.Domain.Settings;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Implementations;
using Steam.Infrastructure.Repositories.Interfaces;
using System.Reflection;
using System.Text;

namespace Steam.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            #region Register AuthSettings
            var authSettings = new AuthSettings();
            builder.Configuration.GetSection("AuthSettings").Bind(authSettings);
            builder.Services.AddSingleton(authSettings);
            #endregion

            #region Register Repositories
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Register Services
            builder.Services.AddScoped<IFileService, CloudinaryService>();
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
            builder.Services.AddScoped<IGiftService, GiftService>();
            builder.Services.AddScoped<IVoucherService, VoucherService>();
            #endregion
            #region Orders Services
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IRefundService, RefundService>();
            #endregion
            #region Library Services
            builder.Services.AddScoped<ILicenseService, LicenseService>();
            builder.Services.AddScoped<IUserLibraryService, UserLibraryService>();
            #endregion
            #region Reviews & Rating Services
            builder.Services.AddScoped<IReviewService, ReviewService>();
            #endregion
            #region Achievements Services
            builder.Services.AddScoped<IAchievementService, AchievementService>();
            builder.Services.AddScoped<ICraftingRecipeService, CraftingRecipeService>();
            builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
            builder.Services.AddScoped<IUserAchievementService, UserAchievementService>();
            builder.Services.AddScoped<IBadgeService, BadgeService>();
            #endregion
            #region Auth Services
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            #endregion
            #endregion

            #region Register AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(CatalogMappingProfile).Assembly);
            });
            #endregion


            // --- IDENTITY CONFIGURATION ---
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();



            // --- JWT AUTHENTICATION CONFIGURATION ---
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["AuthSettings:JwtIssuer"],
                    ValidAudience = builder.Configuration["AuthSettings:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:JwtKey"]))
                };
            });

            var myAllowSpecificOrigins = "AllowSpecificOrigin";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        // React app-in ünvanını bura əlavə edirik
                        builder.WithOrigins("http://localhost:5280", "https://localhost:5281", "http://localhost:5173")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(Assembly.Load("Steam.Application"));
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Swagger sənədi üçün ümumi məlumatlar
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Steam.API",
                    Version = "v1",
                    Description = "A RESTful API for a Steam-like platform"
                });

                // JWT (Bearer) təhlükəsizlik sxemini təyin et
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                                  "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                  "Example: \"Bearer 12345abcdef\""
                });

                // Bütün endpoint'lərin bu təhlükəsizlik sxemini istifadə edə biləcəyini təyin et
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            var app = builder.Build();

            // --- DATA SEEDER EXECUTION ---
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var context = services.GetRequiredService<AppDbContext>(); // AppDbContext'i əldə edin

                await DataSeeder.SeedRolesAndAdminAsync(userManager, roleManager, configuration);

                // YENİ KODU BURAYA ƏLAVƏ EDİN
                await AdditionalDataSeeder.SeedAdditionalDataAsync(context, userManager);
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStaticFiles();
            app.UseCors(myAllowSpecificOrigins);

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
