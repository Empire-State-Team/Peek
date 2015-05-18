using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Peek.Data.UnitOfWork;
using Peek.Web.ViewModels.Products;

namespace Peek.Web.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IPeekData data) 
            : base(data)
        {
        }

        public ActionResult ById(int id)
        {
            var product = this.Data.Products
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<ProductViewModel>()
                .First();

            return this.View("~/Views/Shared/DisplayTemplates/ProductViewModel.cshtml", product);
        }

        public ActionResult ByCategory(int id)
        {
            var products = this.Data.Products
                .All()
                .Where(p => p.InStock && p.CategoryId == id)
                .Project()
                .To<ProductPreviewViewModel>();

            return this.PartialView(products);
        }
    }
}
