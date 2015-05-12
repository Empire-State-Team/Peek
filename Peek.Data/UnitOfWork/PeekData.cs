namespace Peek.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Peek.Data.Repositories;
    using Peek.Models;

    public class PeekData : IPeekData
    {
        private readonly DbContext dbContext;
        private readonly IDictionary<Type, object> repositories;
        private IUserStore<User> userStore;

        public PeekData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<Order> Orders
        {
            get { return this.GetRepository<Order>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IUserStore<User> UserStore
        {
            get { return this.userStore ?? (this.userStore = new UserStore<User>(this.dbContext)); }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var repoType = typeof(Repository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(repoType, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
