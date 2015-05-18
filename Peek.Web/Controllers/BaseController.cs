namespace Peek.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Peek.Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        protected BaseController(IPeekData data)
        {
            this.Data = data;
        }

        protected IPeekData Data { get; private set; }

        protected string CurrentUserId
        {
            get { return this.User.Identity.GetUserId(); }
        }
    }
}
