namespace Peek.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Peek.Models;

    public sealed class Configuration : DbMigrationsConfiguration<PeekDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PeekDbContext context)
        {
            if (!context.Roles.Any() && !context.Users.Any())
            {
                var adminRole = new IdentityRole("Administrator");
                context.Roles.Add(adminRole);

                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var admin = new User { UserName = "admin", Email = "admin@peek.com" };
                manager.Create(admin, "peekadmin");
                manager.AddToRole(admin.Id, "Administrator");
            }
        }
    }
}
