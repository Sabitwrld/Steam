using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Steam.Domain.Entities.Identity;

namespace Steam.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            // 1. Seed Roles ("Admin", "User")
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // 2. Seed the default Admin User from appsettings.json
            var adminUserName = configuration["AdminUser:UserName"];
            if (!userManager.Users.Any(u => u.UserName == adminUserName))
            {
                var adminUser = new AppUser
                {
                    FullName = configuration["AdminUser:FullName"],
                    UserName = adminUserName,
                    Email = configuration["AdminUser:Email"],
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, configuration["AdminUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
