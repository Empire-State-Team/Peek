namespace Peek.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Peek.Data.UnitOfWork;
    using Peek.Web.ViewModels;

    public class HomeController : AdminController
    {
        public HomeController(IPeekData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = this.Data.Categories
                .All()
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            return this.View(categories);
        }
    }
}
