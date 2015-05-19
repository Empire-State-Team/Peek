namespace Peek.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Peek.Common;
    using Peek.Data.UnitOfWork;
    using Peek.Web.Controllers;

    [Authorize(Roles = PeekConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        protected AdminController(IPeekData data)
            : base(data)
        {
        }
    }
}
