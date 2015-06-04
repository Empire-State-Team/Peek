namespace Peek.Web.Controllers
{
    using System.Web.Mvc;
    using Peek.Data.UnitOfWork;
    using Peek.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        public CommentsController(IPeekData data)
            : base(data)
        {
        }

        public ActionResult CreateForProductId(int id)
        {
            return this.PartialView("_AddCommentForm");
        }
    }
}
