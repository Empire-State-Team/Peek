using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Peek.Data.UnitOfWork;
using Peek.Web.Areas.Administration.InputModels;
using Peek.Web.ViewModels;

namespace Peek.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdminController
    {
        public CategoriesController(IPeekData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = this.Data.Categories
                .All()
                .Project()
                .To<CategoryViewModel>();

            return this.View(categories);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = this.Data.Categories
                .All()
                .Where(c => c.Id == id)
                .Project()
                .To<CategoryViewModel>()
                .FirstOrDefault();

            if (category == null)
            {
                throw new HttpException(404, "Category not found");
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(category);
            }

            var dbCategory = this.Data.Categories.Find(category.Id);
            if (dbCategory == null)
            {
                throw new HttpException(404, "Category not found");
            }

            dbCategory.Name = category.Name;
            dbCategory.IsActive = category.IsActive;
            this.Data.Categories.Update(dbCategory);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}
