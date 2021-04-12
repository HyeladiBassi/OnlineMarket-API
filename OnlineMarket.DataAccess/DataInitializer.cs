using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.Models;

namespace OnlineMarket.DataAccess
{
    public class DataInitializer
    {
        public static async Task SeedDatabase(UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
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

            SystemRole sellerRole = new SystemRole
            {
                Name = "seller",
                NormalizedName = "SELLER"
            };

            SystemRole modRole = new SystemRole
            {
                Name = "moderator",
                NormalizedName = "MODERATOR"
            };

            IdentityResult buyerResult = await roleManager.CreateAsync(buyerRole);
            IdentityResult adminResult = await roleManager.CreateAsync(adminRole);
            IdentityResult sellerResult = await roleManager.CreateAsync(sellerRole);
            IdentityResult modResult = await roleManager.CreateAsync(modRole);

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


            if (modResult.Succeeded)
            {
                SystemUser buyer = new SystemUser
                {
                    UserName = "moderator",
                    EmailConfirmed = true,
                    Email = "mod@email.com",
                    FirstName = "Moderator",
                    LastName = "First"
                };
                await userManager.CreateAsync(buyer, "modpass!");
                await userManager.AddToRoleAsync(buyer, "moderator");
            }

            if (sellerResult.Succeeded)
            {
                SystemUser seller = new SystemUser
                {
                    UserName = "seller",
                    EmailConfirmed = true,
                    Email = "seller@email.com",
                    FirstName = "Seller",
                    LastName = "First"
                };

                await userManager.CreateAsync(seller, "sellerpass!");
                await userManager.AddToRoleAsync(seller, "seller");
                Product sample1 = new Product
                {
                    Id = 15,
                    Name = "Sample Product",
                    Currency = "USD",
                    Price = 500,
                    Stock = 50,
                    Description = "Sample description",
                    Category = "Technology",
                    PaymentMethod = "Cash",
                    Seller = seller
                };
                await context.Products.AddAsync(sample1);
                await context.SaveChangesAsync();

                SystemUser seller1 = new SystemUser
                {
                    UserName = "amanda",
                    EmailConfirmed = true,
                    Email = "amanda@email.com",
                    FirstName = "Amanda",
                    LastName = "Wayne"
                };
                await userManager.CreateAsync(seller1, "sellerpass!");
                await userManager.AddToRoleAsync(seller1, "seller");

                SystemUser seller2 = new SystemUser
                {
                    UserName = "ross",
                    EmailConfirmed = true,
                    Email = "ross@email.com",
                    FirstName = "Ross",
                    LastName = "Geller"
                };
                await userManager.CreateAsync(seller2, "sellerpass!");
                await userManager.AddToRoleAsync(seller2, "seller");

                SystemUser seller3 = new SystemUser
                {
                    UserName = "phil",
                    EmailConfirmed = true,
                    Email = "phil@email.com",
                    FirstName = "Phil",
                    LastName = "Dunphy"
                };
                await userManager.CreateAsync(seller3, "sellerpass!");
                await userManager.AddToRoleAsync(seller3, "seller");

                SystemUser seller4 = new SystemUser
                {
                    UserName = "monica",
                    EmailConfirmed = true,
                    Email = "monica@email.com",
                    FirstName = "Monica",
                    LastName = "Geller"
                };
                await userManager.CreateAsync(seller4, "sellerpass!");
                await userManager.AddToRoleAsync(seller4, "seller");

                SystemUser seller5 = new SystemUser
                {
                    UserName = "claire",
                    EmailConfirmed = true,
                    Email = "claire@email.com",
                    FirstName = "Claire",
                    LastName = "Pritchet"
                };
                await userManager.CreateAsync(seller5, "sellerpass!");
                await userManager.AddToRoleAsync(seller5, "seller");
            }


        }
    }
}
