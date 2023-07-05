using City_Vibe.ExtensionMethod;
using City_Vibe.ViewModels.ClubCommentController;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class ClubCommentController : Controller
    {

        private readonly IClubCommentService clubCommentService;
        public ClubCommentController(IClubCommentService _clubCommentService) => clubCommentService = _clubCommentService;


        [HttpPost]
        public ActionResult PostComment(PostCommentClubViewModel comment)
        {
            var request = clubCommentService.PostComment(comment);
            if(!request) return RedirectToAction("Login", "Account");
            return RedirectToAction("PostInformationDetail", "Club", new { postInfoId = comment.PostInfoInClubId });
        }

        [HttpPost]
        public ActionResult PostReply(ReplyCommentClubViewModel commentreply)
        {
            var request = clubCommentService.PostReply(commentreply);
            if(!request) return RedirectToAction("Login", "Account");
            return RedirectToAction( "PostInformationDetail" ,"Club", new {postInfoId = commentreply.PostInfoInClubId });
        }

    }
}
