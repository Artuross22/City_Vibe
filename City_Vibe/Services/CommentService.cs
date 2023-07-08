using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.CommentController;

namespace City_Vibe.Services
{
    public class CommentService : ICommentService
    {
        public readonly IHttpContextAccessor сontextAccessor;
        public readonly IUnitOfWork unitOfWorkRepo;

        public CommentService(IHttpContextAccessor _сontextAccessor, IUnitOfWork _unitOfWorkRepo)
        {
            сontextAccessor = _сontextAccessor;
            unitOfWorkRepo = _unitOfWorkRepo;
        }

        public Response PostComment(PostCommentViewModel comment)
        {
            Response response = new Response();
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;

            if (curUserId == null)
            {
                response.CurrentUser = false;
                return response;
            }

            Comment c = new Comment();
            c.EventId = comment.EventId;
            c.Body = comment.CommentText;
            c.DateTime = DateTime.Now;
            c.ForeignUserId = Guid.Parse(curUserId);
            c.UserName = curUserName;

            var result = unitOfWorkRepo.CommentRepository.Add(c);
            response.Success = result;
            return response;
        }

        public Response PostReply(ReplyViewModel commentreply)
        {
            Response response = new Response();

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            if (curUserId == null)
            {
               response.CurrentUser = false;
               return response;
            }
            ReplyComment r = new ReplyComment();
            r.Text = commentreply.ReplyText;
            r.CommentId = commentreply.IDComment;
            r.InternalUserId = Guid.Parse(curUserId);
            r.CreatedDate = DateTime.Now;
            unitOfWorkRepo.CommentRepository.AddReplyComment(r);

            response.Success = true;
            return response;
        }
    }
}
