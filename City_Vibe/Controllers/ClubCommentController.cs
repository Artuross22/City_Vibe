using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.ClubCommentController;
using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace City_Vibe.Controllers
{
    public class ClubCommentController : Controller
    {

        private readonly IClubCommentRepository commentRepository;
        private readonly UserManager<AppUser> userManager;
        public readonly IHttpContextAccessor сontextAccessor;


        public ClubCommentController(IClubCommentRepository commentRepo, UserManager<AppUser> userMngt, IHttpContextAccessor сontextAccess)
        {
            commentRepository = commentRepo;
            userManager = userMngt;
            сontextAccessor = сontextAccess;
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
            commentRepository.Add(c);
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
            commentRepository.AddReplyComment(r);
            return RedirectToAction( "PostInformationDetail" ,"Club", new {postInfoId = commentreply.PostInfoInClubId });
        }

    }
}
