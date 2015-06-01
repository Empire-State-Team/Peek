namespace Peek.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Peek.Data.UnitOfWork;
    using Peek.Models;
    using Peek.Web.Controllers;
    using Peek.Web.ViewModels.Products;

    [TestClass]
    public class CartControllerTests
    {
        private static Mock<IPeekData> dataMock;
        private static Mock<HttpContextBase> contextMock;
        private static Mock<HttpSessionStateBase> sessionMock;
        private CartController controller;

        [TestInitialize]
        public void TestInit()
        {
            dataMock = new Mock<IPeekData>();
            contextMock = new Mock<HttpContextBase>();
            sessionMock = new Mock<HttpSessionStateBase>();
            contextMock.Setup(c => c.Session).Returns(sessionMock.Object);
            Mapper.CreateMap<Product, ProductPreviewViewModel>();

            this.controller = new CartController(dataMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = contextMock.Object }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestAddUnexistingProductToCart()
        {
            dataMock.Setup(data => data.Products.All()).Returns(Enumerable.Empty<Product>().AsQueryable());
            this.controller.Add(2);
        }

        [TestMethod]
        public void TestAddProductToCartStatus()
        {
            var cart = new List<ProductPreviewViewModel>();
            sessionMock.Setup(s => s["cart"]).Returns(cart);
            dataMock.Setup(data => data.Products.All()).Returns(new[] { new Product { Id = 2 } }.AsQueryable());
            var result = this.controller.Add(2) as ContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Product added to cart.", result.Content);
        }

        [TestMethod]
        public void TestRemoveProductFromCartStatus()
        {
            var cart = new List<ProductPreviewViewModel> { new ProductPreviewViewModel { Id = 2 } };
            sessionMock.Setup(s => s["cart"]).Returns(cart);
            var result = this.controller.Remove(2) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestAddProductToCart()
        {
            var cart = new List<ProductPreviewViewModel>();
            sessionMock.Setup(s => s["cart"]).Returns(cart);
            dataMock.Setup(data => data.Products.All()).Returns(new[] { new Product { Id = 2 } }.AsQueryable());
            this.controller.Add(2);

            Assert.AreEqual(1, cart.Count);
            Assert.IsNotNull(cart.FirstOrDefault(p => p.Id == 2));
            Assert.IsTrue(cart.Any());
        }

        [TestMethod]
        public void TestRemoveProductFromCart()
        {
            var cart = new List<ProductPreviewViewModel> { new ProductPreviewViewModel { Id = 2 } };
            sessionMock.Setup(s => s["cart"]).Returns(cart);
            this.controller.Remove(2);

            Assert.AreEqual(0, cart.Count);
            Assert.IsNull(cart.FirstOrDefault(p => p.Id == 2));
            Assert.IsFalse(cart.Any());
        }
    }
}
