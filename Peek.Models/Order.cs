namespace Peek.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        private ICollection<Product> products;

        public Order()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
