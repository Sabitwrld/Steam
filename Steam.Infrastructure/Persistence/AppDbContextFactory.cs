using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Steam.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=MSI\\SQLEXPRESS03;Database=SteamDB;Trusted_Connection=True;TrustServerCertificate=True";

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseSqlServer(connectionString, opts =>
            {
                opts.MigrationsAssembly("Steam.Infrastructure");
            });

            return new AppDbContext(builder.Options);
        }
    }
}
