using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peek.Data.UnitOfWork;

namespace Peek.Web.Controllers
{
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
