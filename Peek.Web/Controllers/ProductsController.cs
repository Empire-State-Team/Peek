using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Peek.Data.UnitOfWork;
using Peek.Web.Infrastructure.FileStorage;
using Peek.Web.ViewModels.Products;

namespace Peek.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IStorageManager storageManager;

        public ProductsController(IPeekData data, IStorageManager storageManager)
            : base(data)
        {
            this.storageManager = storageManager;
        }

        public ActionResult ById(int id)
        {
            var product = this.Data.Products
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<ProductViewModel>()
                .FirstOrDefault();

            if (product == null)
            {
                throw new HttpException(404, "Product not found");
            }

            if (product.ImagesFolderId != null)
            {
                product.ImageUrls = this.storageManager.GetFileUrls(product.ImagesFolderId);
            }

            return this.View("~/Views/Shared/DisplayTemplates/ProductViewModel.cshtml", product);
        }

        public ActionResult ByCategory(int id)
        {
            var categoryName = this.Data.Categories
                .All()
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();

            var products = this.Data.Products
                .All()
                .Where(p => p.InStock && p.CategoryId == id)
                .Project()
                .To<ProductPreviewViewModel>();

            this.ViewBag.Title = categoryName;
            return this.PartialView("_ProductList", products);
        }

        public ActionResult Latest(int count = 5)
        {
            var products = this.Data.Products
                .All()
                .Where(p => p.InStock)
                .OrderByDescending(p => p.CreatedOn)
                .Take(count)
                .Project()
                .To<ProductPreviewViewModel>();

            this.ViewBag.Title = "Latest products";
            return this.PartialView("_ProductList", products);
        }
    }
}
