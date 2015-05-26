namespace Peek.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel;
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        [DisplayName("Order number")]
        public int Id { get; set; }

        [DisplayName("Created on")]
        public DateTime CreatedOn { get; set; }

        public OrderStatus Status { get; set; }
    }
}
