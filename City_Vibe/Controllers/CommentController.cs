using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.ExtensionMethod;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class CommentController : Controller
    {
        public readonly ICommentService commentController;

        public CommentController(ICommentService _commentController) => commentController = _commentController;


        [HttpPost]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
            var request = commentController.PostComment(comment);
            if (request.CurrentUser == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("DetailEvent", "Event", new { currentEventId = comment.EventId });
        }

        [HttpPost]
        public ActionResult PostReply(ReplyViewModel commentreply)
        {
            var request = commentController.PostReply(commentreply);
            if (request.CurrentUser == false)
            {
                return RedirectToAction("Login", "Account");
            }       
            return RedirectToAction("Index","Event");
        }
    }
}
