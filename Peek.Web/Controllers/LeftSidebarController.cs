﻿namespace Peek.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Peek.Data.UnitOfWork;
    using Peek.Web.ViewModels;

    public class LeftSidebarController : BaseController
    {
        public LeftSidebarController(IPeekData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            var categories = this.Data.Categories
                .All()
                .Where(c => c.IsActive)
                .Project()
                .To<CategoryViewModel>();

            return this.PartialView(new LeftSidebarViewModel { Categories = categories });
        }
    }
}