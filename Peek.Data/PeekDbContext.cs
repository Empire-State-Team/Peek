namespace Peek.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Peek.Models;

    public class PeekDbContext : IdentityDbContext<User>, IPeekDbContext
    {
        public PeekDbContext()
            : base("PeekDbConnection", false)
        {
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static PeekDbContext Create()
        {
            return new PeekDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
