using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace City_Vibe.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<AppUser> userManager;
        public readonly IHttpContextAccessor сontextAccessor;


        public CommentController(ICommentRepository commentRepo, UserManager<AppUser> userMngt , IHttpContextAccessor сontextAccess)
        {
            commentRepository = commentRepo;
            userManager = userMngt;
            сontextAccessor = сontextAccess;
        }

        [HttpPost]
        public ActionResult PostComment(PostCommentViewModel Comment)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            if (curUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Comment c = new Comment();
            c.EventId = Comment.EventId;
            c.Body = Comment.CommentText;
            c.DateTime = DateTime.Now;
            c.ForeignUserId = Guid.Parse(curUserId);
            commentRepository.Add(c);
            return RedirectToAction("DetailEvent", "Event", new { id = c.EventId });
        }

        [HttpPost]
        public ActionResult PostReply(ReplyViewModel commentreply)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            if (curUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ReplyComment r = new ReplyComment();
            r.Text = commentreply.ReplyText;
            r.CommentId = commentreply.IDComment;
            r.InternalUserId = Guid.Parse(curUserId);
            r.CreatedDate = DateTime.Now;
            commentRepository.AddReplyComment(r);
            return RedirectToAction("Index","Event");
        }
    }
}
