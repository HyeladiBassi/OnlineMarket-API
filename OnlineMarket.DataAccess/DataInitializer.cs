using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OnlineMarket.Models;
using JsonNet.ContractResolvers;
using System;
using System.Collections.Generic;

namespace OnlineMarket.DataAccess
{
    public class DataInitializer
    {
        public static void Seed(string jsonData, UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings{
                ContractResolver = new PrivateSetterContractResolver()
            };
            ICollection<Product> data = JsonConvert.DeserializeObject<ICollection<Product>>(jsonData, settings);
            Console.WriteLine(data);
            context.Products.AddRange(data);
        }
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

            Category category1 = new Category
            {
                Id = 1,
                Name = "Technology",
                ParentId = 0
            };

            Category category2 = new Category
            {
                Id = 2,
                Name = "Fashion",
                ParentId = 0
            };

            Category category3 = new Category
            {
                Id = 3,
                Name = "Entertainment",
                ParentId = 0
            };

            Category category4 = new Category
            {
                Id = 4,
                Name = "Appliances",
                ParentId = 0
            };
            await context.Categories.AddAsync(category1);
            await context.Categories.AddAsync(category2);
            await context.Categories.AddAsync(category3);
            await context.Categories.AddAsync(category4);

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

                Product sample1 = new Product
                {
                    Id = 15,
                    Name = "Phone",
                    Currency = "TL",
                    Price = 2000,
                    Stock = 4,
                    Description = "Mobile device",
                    Category = category1,
                    Seller = seller4,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };

                Product sample2 = new Product
                {
                    Id = 16,
                    Name = "Laptop",
                    Currency = "TL",
                    Price = 1000,
                    Stock = 2,
                    Description = "Laptop lorem ipsum",
                    Category = category1,
                    Seller = seller4,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };

                Product sample3 = new Product
                {
                    Id = 17,
                    Name = "Headphones",
                    Currency = "TL",
                    Price = 400,
                    Stock = 5,
                    Description = "Sample description",
                    Category = category1,
                    Seller = seller4,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };

                Product sample4 = new Product
                {
                    Id = 18,
                    Name = "Blue Headphones",
                    Currency = "TL",
                    Price = 500,
                    Stock = 50,
                    Description = "Sample description",
                    Category = category1,
                    Seller = seller4,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };

                Product sample5 = new Product
                {
                    Id = 19,
                    Name = "Bag",
                    Currency = "TL",
                    Price = 500,
                    Stock = 50,
                    Description = "Sample description",
                    Category = category2,
                    Seller = seller4,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };

                Product sample6 = new Product
                {
                    Id = 20,
                    Name = "Monitor",
                    Currency = "TL",
                    Price = 500,
                    Stock = 50,
                    Description = "Sample description",
                    Category = category1,
                    Seller = seller3,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };

                Product sample7 = new Product
                {
                    Id = 21,
                    Name = "Book",
                    Currency = "TL",
                    Price = 500,
                    Stock = 50,
                    Description = "Book lorem ipsum",
                    Category = category3,
                    Seller = seller3,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                await context.Products.AddAsync(sample1);
                await context.Products.AddAsync(sample2);
                await context.Products.AddAsync(sample3);
                await context.Products.AddAsync(sample4);
                await context.Products.AddAsync(sample5);
                await context.Products.AddAsync(sample6);
                await context.Products.AddAsync(sample7);
                await context.SaveChangesAsync();
            }
            


        }
    }
}
