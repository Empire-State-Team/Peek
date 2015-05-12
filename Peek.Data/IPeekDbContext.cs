namespace Peek.Data
{
    using System.Data.Entity;
    using Peek.Models;

    public interface IPeekDbContext
    {
        IDbSet<User> Users { get; }

        IDbSet<Category> Categories { get; }

        IDbSet<Product> Products { get; }

        IDbSet<Order> Orders { get; }

        IDbSet<Comment> Comments { get; } 
    }
}
