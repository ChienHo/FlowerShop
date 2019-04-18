using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.FlowerShop.Core
{
    class FlowerShopInitializer : DropCreateDatabaseIfModelChanges<FlowerShopContext>
    {
        protected override void Seed(FlowerShopContext context)
        {
            SeedData(context);
            base.Seed(context);
            Task.Run(async () => { await SeedAsync(context); }).Wait();
        }
        private static void SeedData(FlowerShopContext context)
        {
            List<Category> categories = new List<Category>
            {
                new Category()
                {
                    CategoryName = "Category 01",
                    Notes = "Note for category 01"
                },
                new Category()
                {
                    CategoryName = "Category 02",
                    Notes = "Note for category 02"
                },
                new Category()
                {
                    CategoryName = "Category 03",
                    Notes = "Note for category 03"
                }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var flowers = new List<Flower>()
            {
                new Flower()
                {
                    FlowerName = "Flower 01",
                    Description = "Description for flower 01",
                    ImageUrl = "flower001.jpg",
                    Price = 10M,
                    Color = Color.Blue,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 01")
                },
                new Flower()
                {
                    FlowerName = "Flower 02",
                    Description = "Description for flower 02",
                    ImageUrl = "flower002.jpg",
                    Price = 9M,
                    Color = Color.Red,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 01")
                },
                new Flower()
                {
                    FlowerName = "Flower 03",
                    Description = "Description for flower 03",
                    ImageUrl = "flower003.jpg",
                    Price = 11.99M,
                    Color = Color.Pink,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 02")
                },
                new Flower()
                {
                    FlowerName = "Flower 04",
                    Description = "Description for flower 04",
                    ImageUrl = "flower004.jpg",
                    Price = 12.99M,
                    Color = Color.Purple,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 02")
                },
                new Flower()
                {
                    FlowerName = "Flower 05",
                    Description = "Description for flower 05",
                    ImageUrl = "flower005.jpg",
                    Price = 10M,
                    Color = Color.Blue,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 03")
                },
                new Flower()
                {
                    FlowerName = "Flower 06",
                    Description = "Description for flower 06",
                    ImageUrl = "flower006.jpg",
                    Price = 1.99M,
                    Color = Color.Blue,
                    StoreDate = DateTimeOffset.Now,
                    StoreInventory = 2,
                    Category = categories.Single(c=>c.CategoryName=="Category 03")
                },
                new Flower()
                {
                FlowerName = "Flower 07",
                Description = "Description for flower 07",
                ImageUrl = "flower007.jpg",
                Price = 1.99M,
                Color = Color.Lavender,
                StoreDate = DateTimeOffset.Now,
                StoreInventory = 2,
                Category = categories.Single(c=>c.CategoryName=="Category 03")
            }
            };
            context.Flowers.AddRange(flowers);
            context.SaveChanges();
        }
        #region Add User and Role Identity

        private async Task SeedAsync(FlowerShopContext context)
        {


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Administrators"));
                await roleManager.CreateAsync(new IdentityRole("Contributor"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!userManager.Users.Any(u => u.UserName == "admin@domain.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@domain.com",
                    UserName = "admin@domain.com",
                    PasswordHash = passwordHasher.HashPassword("Abc@1234"),
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "Administrators");
                await userManager.AddToRoleAsync(user.Id, "Contributor");
                await userManager.AddToRoleAsync(user.Id, "User");
            }

            if (!userManager.Users.Any(u => u.UserName == "Chien@domain.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "cong@domain.com",
                    UserName = "cong@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "Administrators");
            }

            if (!userManager.Users.Any(u => u.UserName == "van@domain.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "van@domain.com",
                    UserName = "van@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "Contributor");
            }

            if (!userManager.Users.Any(u => u.UserName == "quynh@domain.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "quynh@domain.com",
                    UserName = "quynh@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "User");
            }

            if (!userManager.Users.Any(u => u.UserName == "customer@domain.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "customer@domain.com",
                    UserName = "customer@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "User");
            }
        }
    }
    #endregion
}