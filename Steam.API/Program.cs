
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Application.Profiles;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Implementations;
using Steam.Infrastructure.Repositories.Implementations.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

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
            #endregion
            #endregion

            #region Register Services
            #region Catalog Services
            builder.Services.AddScoped<IApplicationCatalogService, ApplicationCatalogService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            #endregion
            #endregion

            #region Register AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(CatalogProfile).Assembly);
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
