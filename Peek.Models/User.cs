namespace Peek.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Order> orders;
        private ICollection<Comment> comments;

        public User()
        {
            this.orders = new HashSet<Order>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
