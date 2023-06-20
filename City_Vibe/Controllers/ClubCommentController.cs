using City_Vibe.ExtensionMethod;
using City_Vibe.ViewModels.ClubCommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;

namespace City_Vibe.Controllers
{
    public class ClubCommentController : Controller
    {

        private readonly IUnitOfWork unitOfWorkRepo;
        public readonly IHttpContextAccessor сontextAccessor;

        public ClubCommentController(
            IHttpContextAccessor сontextAccess,
            IUnitOfWork unitOfWorkRepository)
        {
            сontextAccessor = сontextAccess;
            unitOfWorkRepo = unitOfWorkRepository;
        }

        [HttpPost]
        public ActionResult PostComment(PostCommentClubViewModel comment)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;
            if (curUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CommentClub c = new CommentClub();
            c.PostInfoInClubId = comment.PostInfoInClubId;
            c.Body = comment.CommentText;
            c.DateTime = DateTime.Now;
            c.ForeignUserId = Guid.Parse(curUserId);
            c.UserName = c.UserName;
            unitOfWorkRepo.ClubCommentRepository.Add(c);
            return RedirectToAction("PostInformationDetail", "Club" , new { postInfoId = comment.PostInfoInClubId });
        }

        [HttpPost]
        public ActionResult PostReply(ReplyCommentClubViewModel commentreply)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;
            if (curUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ReplyCommentClub r = new ReplyCommentClub();
            r.Text = commentreply.ReplyText;
            r.CommentClubId = commentreply.IdComment;
            r.InternalUserId = Guid.Parse(curUserId);
            r.CreatedDate = DateTime.Now;
            r.UserName = curUserName;
            unitOfWorkRepo.ClubCommentRepository.AddReplyComment(r);
            

            return RedirectToAction( "PostInformationDetail" ,"Club", new {postInfoId = commentreply.PostInfoInClubId });
        }

    }
}
