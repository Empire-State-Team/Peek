namespace Peek.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Peek.Models;
    using Peek.Common;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<PeekDbContext>
    {
        private readonly IRandomGenerator random;
        private UserManager<User> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(PeekDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));

            this.SeedUsers(context);
            this.SeedCategories(context);
            this.SeedProductsWithComments(context);
            this.SeedOrders(context);
        }

        private void SeedOrders(PeekDbContext context)
        {
            if (context.Orders.Any())
            {
                return;
            }

            var users = context.Users.ToList();
            for (var i = 0; i < 20; i++)
            {
                var products = new List<Product>();
                foreach (var product in products)
                {
                    if (this.random.RandomNumber(0, 100) % 2 == 0)
                    {
                        products.Add(product);
                    }
                }

                var order = new Order
                {
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), this.random.RandomNumber(0, 2).ToString()),
                    User = users[this.random.RandomNumber(0, users.Count() - 1)],
                    Products = products,
                    CreatedOn = DateTime.Now - new TimeSpan(this.random.RandomNumber(1, 500), 0, 0)
                };

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        private void SeedProductsWithComments(PeekDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var categories = context.Categories.ToList();
            var users = context.Users.ToList();

            for (var i = 0; i < 30; i++)
            {
                var product = new Product
                {
                    Name = this.random.RandomString(5, 15),
                    Description = this.random.RandomString(100, 1000),
                    Price = this.random.RandomNumber(1, 120),
                    Category = categories[this.random.RandomNumber(1, 100) % categories.Count],
                    CreatedOn = DateTime.Now - new TimeSpan(this.random.RandomNumber(1, 500), 0, 0),
                    CreatedUser = users[this.random.RandomNumber(1, users.Count() - 1)],
                    InStock = this.random.RandomNumber(0, 10) % 2 == 0
                };

                for (var k = 0; k < 40; k++)
                {
                    var comment = new Comment
                    {
                        PostedAt = DateTime.Now,
                        Product = product,
                        Text = this.random.RandomString(20, 1000),
                        User = users[this.random.RandomNumber(0, users.Count - 1)]
                    };

                    context.Comments.Add(comment);

                    if (this.random.RandomNumber(5, 10) > 7)
                    {
                        break;
                    }
                }

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        private void SeedCategories(PeekDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category
                {
                    Name = "Mobile Phones",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedUser = context.Users.First()
                });

                context.Categories.Add(new Category
                {
                    Name = "Hardware",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedUser = context.Users.First()
                });

                context.Categories.Add(new Category
                {
                    Name = "Cars",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedUser = context.Users.ToList()[this.random.RandomNumber(0, context.Users.Count() - 1)]
                });

                context.Categories.Add(new Category
                {
                    Name = "Other",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedUser = context.Users.First()
                });

                context.Categories.Add(new Category
                {
                    Name = this.random.RandomString(5, 15),
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedUser = context.Users.ToList()[this.random.RandomNumber(0, context.Users.Count() - 1)]
                });

                context.SaveChanges();
            }
        }

        private void SeedUsers(PeekDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var adminRole = new IdentityRole(PeekConstants.AdminRole);
            context.Roles.Add(adminRole);
            var admin = new User { UserName = PeekConstants.AdminUsername, Email = "admin@peek.com" };
            this.userManager.Create(admin, "peekadmin");
            this.userManager.AddToRole(admin.Id, PeekConstants.AdminRole);

            for (var i = 0; i < 15; i++)
            {
                var user = new User
                {
                    Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16), this.random.RandomString(6, 16)),
                    UserName = this.random.RandomString(6, 16)
                };

                this.userManager.Create(user, "123456");
            }
        }
    }
}
