using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peek.Data.UnitOfWork;
using Peek.Web.Areas.Administration.InputModels;

namespace Peek.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        public ProductsController(IPeekData data)
            : base(data)
        {
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

            return this.View();
        }

        private void AddCategoriesToViewBag()
        {
            var categories = this.Data.Categories.All().Select(c => new { c.Id, c.Name });
            this.ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
