namespace Peek.Data.UnitOfWork
{
    using Microsoft.AspNet.Identity;
    using Peek.Data.Repositories;
    using Peek.Models;

    public interface IPeekData
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Product> Products { get; }

        IRepository<Order> Orders { get; }

        IRepository<Comment> Comments { get; }

        IUserStore<User> UserStore { get; }

        void SaveChanges();
    }
}
