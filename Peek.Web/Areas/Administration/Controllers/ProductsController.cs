namespace Peek.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
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
            return this.View(new ProductInputModel());
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

        private void AddCategoriesToViewBag()
        {
            var categories = this.Data.Categories.All().Select(c => new { c.Id, c.Name });
            this.ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
