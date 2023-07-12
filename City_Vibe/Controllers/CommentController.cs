using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class CommentController : Controller
    {
        public readonly ICommentService commentService;

        public CommentController(ICommentService _commentController) => commentService = _commentController;



        [HttpPost]
        public ActionResult PostComment(PostCommentViewModel comment)
        {

            var request = commentService.PostComment(comment);
            if (request.CurrentUser == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("DetailEvent", "Event", new { currentEventId = comment.EventId });
        }

        [HttpPost]
        public ActionResult PostReply(ReplyViewModel commentreply)
        {
            var request = commentService.PostReply(commentreply);
            if (request.CurrentUser == false)
            {
                return RedirectToAction("Login", "Account");
            }       
            return RedirectToAction("Index","Event");
        }
    }
}
