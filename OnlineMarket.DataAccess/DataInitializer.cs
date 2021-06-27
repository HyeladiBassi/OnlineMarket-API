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

            Category electronics = new Category
            {
                Id = 1,
                Name = "Electronics",
                ParentId = 0
            };
            Category fashion = new Category
            {
                Id = 2,
                Name = "Fashion",
                ParentId = 0
            };
            Category entertainment = new Category
            {
                Id = 3,
                Name = "Entertainment",
                ParentId = 0
            };
            Category household = new Category
            {
                Id = 4,
                Name = "Household",
                ParentId = 0
            };
            Category appliances = new Category
            {
                Id = 5,
                Name = "Appliances",
                ParentId = 0
            };
            Category office = new Category
            {
                Id = 6,
                Name = "Office",
                ParentId = 0
            };
            Category electronics1 = new Category
            {
                Id = 7,
                Name = "Computer and Tablet",
                ParentId = 1
            };
            Category electronics2 = new Category
            {
                Id = 8,
                Name = "Smartphones",
                ParentId = 1
            };
            Category electronics3 = new Category
            {
                Id = 9,
                Name = "TV and Audio",
                ParentId = 1
            };
            Category electronics4 = new Category
            {
                Id = 10,
                Name = "Data Storage",
                ParentId = 1
            };
            Category fashion1 = new Category
            {
                Id = 11,
                Name = "Shoes and Bags",
                ParentId = 2
            };
            Category fashion2 = new Category
            {
                Id = 12,
                Name = "Clothing",
                ParentId = 2
            };
            Category fashion3 = new Category
            {
                Id = 13,
                Name = "Sports",
                ParentId = 2
            };
            Category fashion4 = new Category
            {
                Id = 14,
                Name = "Jewelry",
                ParentId = 2
            };
            Category fashion5 = new Category
            {
                Id = 15,
                Name = "Winter Gear",
                ParentId = 2
            };
            Category entertainment1 = new Category
            {
                Id = 16,
                Name = "Games and Console",
                ParentId = 3
            };
            Category entertainment2 = new Category
            {
                Id = 17,
                Name = "Books",
                ParentId = 3
            };
            Category entertainment3 = new Category
            {
                Id = 18,
                Name = "Board Games",
                ParentId = 3
            };
            Category household1 = new Category
            {
                Id = 19,
                Name = "Furniture",
                ParentId = 4
            };
            Category household2 = new Category
            {
                Id = 20,
                Name = "Decoration",
                ParentId = 4
            };
            Category household3 = new Category
            {
                Id = 21,
                Name = "Wall Prints",
                ParentId = 4
            };
            Category household4 = new Category
            {
                Id = 22,
                Name = "Bedroom",
                ParentId = 4
            };
            Category household5 = new Category
            {
                Id = 23,
                Name = "Bathroom",
                ParentId = 4
            };
            Category household6 = new Category
            {
                Id = 24,
                Name = "Kitchen",
                ParentId = 4
            };
            Category household7 = new Category
            {
                Id = 25,
                Name = "Nursery",
                ParentId = 4
            };
            Category household8 = new Category
            {
                Id = 26,
                Name = "Storage",
                ParentId = 4
            };
            Category appliances1 = new Category
            {
                Id = 27,
                Name = "Kettles",
                ParentId = 5
            };
            Category appliances2 = new Category
            {
                Id = 28,
                Name = "Microwaves",
                ParentId = 5
            };
            Category appliances3 = new Category
            {
                Id = 29,
                Name = "Fridges",
                ParentId = 5
            };
            Category appliances4 = new Category
            {
                Id = 30,
                Name = "Laundry",
                ParentId = 5
            };
            Category appliances5 = new Category
            {
                Id = 31,
                Name = "Other",
                ParentId = 5
            };
            Category office1 = new Category
            {
                Id = 32,
                Name = "Stationary",
                ParentId = 6
            };
            Category office2 = new Category
            {
                Id = 33,
                Name = "Desktop",
                ParentId = 6
            };
            Category office3 = new Category
            {
                Id = 34,
                Name = "Organization",
                ParentId = 6
            };
            Category office4 = new Category
            {
                Id = 35,
                Name = "Productivity",
                ParentId = 6
            };

            await context.Categories.AddAsync(electronics);
            await context.Categories.AddAsync(electronics1);
            await context.Categories.AddAsync(electronics2);
            await context.Categories.AddAsync(electronics3);

            await context.Categories.AddAsync(fashion);
            await context.Categories.AddAsync(fashion1);
            await context.Categories.AddAsync(fashion2);
            await context.Categories.AddAsync(fashion3);
            await context.Categories.AddAsync(fashion4);
            await context.Categories.AddAsync(fashion5);

            await context.Categories.AddAsync(entertainment);
            await context.Categories.AddAsync(entertainment1);
            await context.Categories.AddAsync(entertainment2);
            await context.Categories.AddAsync(entertainment3);

            await context.Categories.AddAsync(household);
            await context.Categories.AddAsync(household1);
            await context.Categories.AddAsync(household2);
            await context.Categories.AddAsync(household3);
            await context.Categories.AddAsync(household4);
            await context.Categories.AddAsync(household5);
            await context.Categories.AddAsync(household6);
            await context.Categories.AddAsync(household7);
            await context.Categories.AddAsync(household8);

            await context.Categories.AddAsync(appliances);
            await context.Categories.AddAsync(appliances1);
            await context.Categories.AddAsync(appliances2);
            await context.Categories.AddAsync(appliances3);
            await context.Categories.AddAsync(appliances4);
            await context.Categories.AddAsync(appliances5);

            await context.Categories.AddAsync(office);
            await context.Categories.AddAsync(office1);
            await context.Categories.AddAsync(office2);
            await context.Categories.AddAsync(office3);
            await context.Categories.AddAsync(office4);

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
                    Description = "Mobile device Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics2,
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
                    Category = electronics2,
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
                    Category = electronics3,
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
                    Category = electronics3,
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
                    Category = fashion1,
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
                    Category = electronics1,
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
                    Category = entertainment2,
                    Seller = seller3,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample8 = new Product
                {
                    Id = 22,
                    Name = "White board",
                    Currency = "TL",
                    Price = 40,
                    Stock = 3,
                    Description = "White board Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = office3,
                    Seller = seller1,
                    WarehouseLocation = "Lefke",
                    AverageRating = 0,
                };
                Product sample9 = new Product
                {
                    Id = 23,
                    Name = "Candle",
                    Currency = "TL",
                    Price = 59,
                    Stock = 50,
                    Description = "Candle Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household2,
                    Seller = seller1,
                    WarehouseLocation = "Nicosia",
                    AverageRating = 0,
                };
                Product sample10 = new Product
                {
                    Id = 24,
                    Name = "Mirror",
                    Currency = "TL",
                    Price = 100,
                    Stock = 1,
                    Description = "Mirror Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household2,
                    Seller = seller1,
                    WarehouseLocation = "Lefke",
                    AverageRating = 0,
                };
                Product sample11 = new Product
                {
                    Id = 25,
                    Name = "Vase",
                    Currency = "TL",
                    Price = 90,
                    Stock = 50,
                    Description = "Vase Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household2,
                    Seller = seller1,
                    WarehouseLocation = "Karpaz",
                    AverageRating = 0,
                };
                Product sample12 = new Product
                {
                    Id = 26,
                    Name = "Scented Candle",
                    Currency = "TL",
                    Price = 39,
                    Stock = 50,
                    Description = "Scented Candle Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household2,
                    Seller = seller3,
                    WarehouseLocation = "Lefke",
                    AverageRating = 0,
                };
                Product sample13 = new Product
                {
                    Id = 27,
                    Name = "Laptop 2",
                    Currency = "TL",
                    Price = 1400,
                    Stock = 2,
                    Description = "Laptop 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller3,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample14 = new Product
                {
                    Id = 28,
                    Name = "Laptop 3",
                    Currency = "TL",
                    Price = 2200,
                    Stock = 3,
                    Description = "Laptop 3 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller3,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample15 = new Product
                {
                    Id = 29,
                    Name = "Laptop 4",
                    Currency = "TL",
                    Price = 999,
                    Stock = 1,
                    Description = "Laptop 4 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller5,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };
                Product sample16 = new Product
                {
                    Id = 30,
                    Name = "Laptop 5",
                    Currency = "TL",
                    Price = 2200,
                    Stock = 3,
                    Description = "Laptop 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller3,
                    WarehouseLocation = "Famagusta",
                    AverageRating = 0,
                };
                Product sample17 = new Product
                {
                    Id = 31,
                    Name = "Tablet",
                    Currency = "TL",
                    Price = 500,
                    Stock = 30,
                    Description = "Tablet Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller2,
                    WarehouseLocation = "Lefke",
                    AverageRating = 0,
                };
                Product sample18 = new Product
                {
                    Id = 32,
                    Name = "Tablet 2",
                    Currency = "TL",
                    Price = 2200,
                    Stock = 3,
                    Description = "Tablet 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = electronics1,
                    Seller = seller1,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample19 = new Product
                {
                    Id = 33,
                    Name = "Mug",
                    Currency = "TL",
                    Price = 19,
                    Stock = 3,
                    Description = "Mug Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample20 = new Product
                {
                    Id = 34,
                    Name = "Coffee mug",
                    Currency = "TL",
                    Price = 41,
                    Stock = 3,
                    Description = "Coffee mug Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample21 = new Product
                {
                    Id = 35,
                    Name = "Tea mug 2",
                    Currency = "TL",
                    Price = 15,
                    Stock = 3,
                    Description = "Tea mug 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
                    WarehouseLocation = "Lefke",
                    AverageRating = 0,
                };
                Product sample22 = new Product
                {
                    Id = 36,
                    Name = "Mug 2",
                    Currency = "TL",
                    Price = 45,
                    Stock = 3,
                    Description = "Mug 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample23 = new Product
                {
                    Id = 37,
                    Name = "Coffee mug 2",
                    Currency = "TL",
                    Price = 30,
                    Stock = 3,
                    Description = "Coffee mug 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
                    WarehouseLocation = "Kyrenia",
                    AverageRating = 0,
                };
                Product sample24 = new Product
                {
                    Id = 38,
                    Name = "Tea mug 2",
                    Currency = "TL",
                    Price = 12,
                    Stock = 3,
                    Description = "Tea mug 2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam a lectus eget nisi commodo ullamcorper ac in ante.",
                    Category = household1,
                    Seller = seller1,
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
                await context.Products.AddAsync(sample8);
                await context.Products.AddAsync(sample9);
                await context.Products.AddAsync(sample10);
                await context.Products.AddAsync(sample11);
                await context.Products.AddAsync(sample12);
                await context.Products.AddAsync(sample13);
                await context.Products.AddAsync(sample14);
                await context.Products.AddAsync(sample15);
                await context.Products.AddAsync(sample16);
                await context.Products.AddAsync(sample17);
                await context.Products.AddAsync(sample18);
                await context.Products.AddAsync(sample19);
                await context.Products.AddAsync(sample20);
                await context.Products.AddAsync(sample21);
                await context.Products.AddAsync(sample22);
                await context.Products.AddAsync(sample23);
                await context.Products.AddAsync(sample24);
                await context.SaveChangesAsync();
            }



        }
    }
}
