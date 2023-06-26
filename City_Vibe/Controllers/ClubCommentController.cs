using City_Vibe.ExtensionMethod;
using City_Vibe.ViewModels.ClubCommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;

namespace City_Vibe.Controllers
{
    public class ClubCommentController : Controller
    {

        private readonly IUnitOfWork unitOfWorkRepository;
        public readonly IHttpContextAccessor сontextAccessor;
        private readonly IMapper mapper;

        public ClubCommentController(
            IHttpContextAccessor сontextAccess,
            IUnitOfWork unitOfWorkRepo,
            IMapper mapp
            )

        {
            сontextAccessor = сontextAccess;
            unitOfWorkRepository = unitOfWorkRepo;
            mapper = mapp;
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

            var commentClub = mapper.Map<CommentClub>(comment);
            commentClub.ForeignUserId = Guid.Parse(curUserId);
            commentClub.DateTime = DateTime.Now;
            commentClub.UserName = curUserName;

            unitOfWorkRepository.ClubCommentRepository.Add(commentClub);
            unitOfWorkRepository.Save();
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
            var comment = mapper.Map<ReplyCommentClub>(commentreply);
            comment.InternalUserId = Guid.Parse(curUserId);
            comment.CreatedDate = DateTime.Now;
            comment.UserName = curUserName;

            unitOfWorkRepository.ClubCommentRepository.AddReplyComment(comment);

            return RedirectToAction( "PostInformationDetail" ,"Club", new {postInfoId = commentreply.PostInfoInClubId });
        }

    }
}
