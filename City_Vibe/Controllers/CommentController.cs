using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.ExtensionMethod;

namespace City_Vibe.Controllers
{
    public class CommentController : Controller
    {
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly IUnitOfWork unitOfWorkRepo;


        public CommentController(IHttpContextAccessor сontextAccess, IUnitOfWork unitOfWorkRepository)
        {
            сontextAccessor = сontextAccess;
            unitOfWorkRepo = unitOfWorkRepository;
        }

        [HttpPost]
        public ActionResult PostComment(PostCommentViewModel Comment)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;

            if (curUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Comment c = new Comment();
            c.EventId = Comment.EventId;
            c.Body = Comment.CommentText;
            c.DateTime = DateTime.Now;
            c.ForeignUserId = Guid.Parse(curUserId);
            c.UserName = curUserName;

            unitOfWorkRepo.CommentRepository.Add(c);
            unitOfWorkRepo.Save();
            return RedirectToAction("DetailEvent", "Event", new { currentEventId = c.EventId });
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
            unitOfWorkRepo.CommentRepository.AddReplyComment(r);
            unitOfWorkRepo.Save();
            return RedirectToAction("Index","Event");
        }
    }
}
