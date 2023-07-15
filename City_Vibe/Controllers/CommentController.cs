using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Mvc;
using City_Vibe.Contracts;
using City_Vibe.ValidationAttribute.BaseFilters;

namespace City_Vibe.Controllers
{
    [ValidateModelAttribute]
    [ValidateGetUserIdAttribute]
    public class CommentController : Controller
    {
        public readonly ICommentService commentService;

        public CommentController(ICommentService _commentController) => commentService = _commentController;

        [HttpPost]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
           var result =  commentService.PostComment(comment);
            if(result) return RedirectToAction("DetailEvent", "Event", new { currentEventId = comment.EventId });

            return View(comment);
        }

        [HttpPost]
        public ActionResult PostReply(ReplyViewModel commentreply)
        {
         var result = commentService.PostReply(commentreply); 
         if(result) return RedirectToAction("Index","Event");

         return View(commentreply);
        }

    }
}
