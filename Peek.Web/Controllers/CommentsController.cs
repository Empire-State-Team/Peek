namespace Peek.Web.Controllers
{
    using System.Web.Mvc;

    using Peek.Data.UnitOfWork;

    public class CommentsController : BaseController
    {
        public CommentsController(IPeekData data)
            : base(data)
        {
        }

        public ActionResult CreateForProductId(int id)
        {
            return PartialView("_CreateCommentForProduct");
        }
    }
}