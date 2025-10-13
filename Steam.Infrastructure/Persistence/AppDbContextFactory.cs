using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Bu metod yalnız 'update-database' və 'add-migration' üçün işləyir.
            // Bu səbəbdən connection string-i birbaşa burada təyin etmək daha etibarlıdır.

            // DİQQƏT: Connection string-i öz lokal bazanıza uyğun olaraq buraya yazın.
            // Bu məlumatı appsettings.json faylından kopyalaya bilərsiniz.
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
