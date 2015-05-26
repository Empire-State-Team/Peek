﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Peek.Data.UnitOfWork;
using Peek.Models;
using Peek.Web.ViewModels;
using Peek.Web.ViewModels.Orders;
using Peek.Web.ViewModels.Products;

namespace Peek.Web.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        public CartController(IPeekData data)
            : base(data)
        {
        }

        private ICollection<ProductPreviewViewModel> Cart
        {
            get
            {
                if (this.HttpContext.Session["cart"] == null)
                {
                    this.HttpContext.Session["cart"] = new List<ProductPreviewViewModel>();
                }

                return (ICollection<ProductPreviewViewModel>)this.HttpContext.Session["cart"];
            }
        }

        public ActionResult Index()
        {
            return this.View(this.Cart);
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            var product = this.Data.Products
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<ProductPreviewViewModel>()
                .FirstOrDefault();

            if (product == null)
            {
                throw new HttpException(404, "Product not found");
            }

            this.Cart.Add(product);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var product = this.Cart.FirstOrDefault(p => p.Id == id);
            this.Cart.Remove(product);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Order()
        {
            var productIds = this.Cart.Select(p => p.Id);
            var products = this.Data.Products
                .All()
                .Where(p => productIds.Contains(p.Id))
                .ToList();
            var order = new Order
            {
                Products = products,
                UserId = this.CurrentUserId,
                CreatedOn = DateTime.Now,
                Status = OrderStatus.Pending
            };

            this.Data.Orders.Add(order);
            this.Data.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult OrderHistory()
        {
            var orders = this.Data.Orders
                .All()
                .Where(o => o.UserId == this.CurrentUserId)
                .Project()
                .To<OrderViewModel>();

            return this.View(orders);
        }
    }
}
