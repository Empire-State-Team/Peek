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
    using Peek.Web.ViewModels;

    public class CategoriesController : AdminController
    {
        public CategoriesController(IPeekData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View(new CategoryViewModel { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(category);
            }

            var dbCategory = Mapper.Map<Category>(category);
            dbCategory.CreatedOn = DateTime.Now;
            dbCategory.CreatedUserId = this.CurrentUserId;
            this.Data.Categories.Add(dbCategory);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index", "Categories");
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

            return this.RedirectToAction("Index", "Categories");
        }
    }
}
