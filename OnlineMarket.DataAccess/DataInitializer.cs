using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.Models;

namespace OnlineMarket.DataAccess
{
    public class DataInitializer
    {
        public static async Task SeedDatabase(UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SystemRole buyerRole = new SystemRole
            {
                Name = "buyer",
                NormalizedName = "BUYER"
            };

            SystemRole adminRole = new SystemRole
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            IdentityResult buyerResult = await roleManager.CreateAsync(buyerRole);
            IdentityResult adminResult = await roleManager.CreateAsync(adminRole);

            if (adminResult.Succeeded)
            {
                SystemUser admin = new SystemUser
                {
                    UserName = "admin",
                    EmailConfirmed = true,
                    Email = "admin@email.com",
                    FirstName = "AdminFirstName"
                };
                await userManager.CreateAsync(admin, "adminpass!");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            if (buyerResult.Succeeded)
            {
                SystemUser buyer = new SystemUser
                {
                    UserName = "buyer",
                    EmailConfirmed = true,
                    Email = "buyer@email.com",
                    FirstName = "Buyer",
                    LastName = "First"
                };
                await userManager.CreateAsync(buyer, "buyerpass!");
                await userManager.AddToRoleAsync(buyer, "buyer");
            }

        }
    }
}