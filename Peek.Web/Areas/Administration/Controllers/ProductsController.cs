namespace Peek.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Peek.Data.UnitOfWork;
    using Peek.Models;
    using Peek.Web.Areas.Administration.InputModels;
    using Peek.Web.Infrastructure.FileStorage;

    public class ProductsController : AdminController
    {
        private readonly IStorageManager storageManager;

        public ProductsController(IPeekData data, IStorageManager storageManager)
            : base(data)
        {
            this.storageManager = storageManager;
        }

        [HttpGet]
        public ActionResult Add()
        {
            this.AddCategoriesToViewBag();
            return this.View(new ProductInputModel { InStock = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductInputModel product)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddCategoriesToViewBag();
                return this.View(product);
            }

            var dbProduct = Mapper.Map<Product>(product);
            dbProduct.CreatedOn = DateTime.Now;
            dbProduct.CreatedUserId = this.CurrentUserId;

            if (product.Images != null && product.Images.FirstOrDefault() != null)
            {
                var folderId = this.storageManager.UploadProductImages(product);
                dbProduct.ImagesFolderId = folderId;
            }

            this.Data.Products.Add(dbProduct);
            this.Data.SaveChanges();

            return this.RedirectToAction("ById", "Products", new { id = dbProduct.Id, area = string.Empty });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = this.Data.Products
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<ProductInputModel>()
                .FirstOrDefault();

            if (product == null)
            {
                throw new HttpException(404, "Product not found");
            }

            this.AddCategoriesToViewBag();
            return this.View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductInputModel product)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddCategoriesToViewBag();
                return this.View(product);
            }

            var dbProduct = this.Data.Products.Find(product.Id);
            if (dbProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.InStock = product.InStock;
            dbProduct.Price = product.Price;
            this.Data.Products.Update(dbProduct);
            this.Data.SaveChanges();

            return this.RedirectToAction("ById", "Products", new { id = dbProduct.Id, area = string.Empty });
        }

        private void AddCategoriesToViewBag()
        {
            var categories = this.Data.Categories.All().Select(c => new { c.Id, c.Name });
            this.ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
