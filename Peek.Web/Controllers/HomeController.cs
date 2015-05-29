namespace Peek.Web.Controllers
{
    using System.Web.Mvc;
    using Peek.Data.UnitOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IPeekData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [ActionName("Profile")]
        public ActionResult UserProfile()
        {
            return this.View();
        }
    }
}
