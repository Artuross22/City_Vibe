using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.ViewModels.ClubCommentController;

namespace City_Vibe.Services
{

    public class ClubCommentService : IClubCommentService
    {

        private readonly IUnitOfWork unitOfWorkRepository;
        public readonly IHttpContextAccessor сontextAccessor;
        private readonly IMapper mapper;

        public ClubCommentService(
            IHttpContextAccessor сontextAccess,
            IUnitOfWork unitOfWorkRepo,
            IMapper mapp)

        {
            сontextAccessor = сontextAccess;
            unitOfWorkRepository = unitOfWorkRepo;
            mapper = mapp;
        }

        public bool PostComment(PostCommentClubViewModel comment)
        {
            var curUserId = сontextAccessor?.HttpContext?.User.GetUserId();
            var curUserName = сontextAccessor?.HttpContext?.User?.Identity?.Name;

            var commentClub = mapper.Map<CommentClub>(comment);
            commentClub.ForeignUserId = Guid.Parse(curUserId);
            commentClub.DateTime = DateTime.Now;
            commentClub.UserName = curUserName;

          var response = unitOfWorkRepository.ClubCommentRepository.Add(commentClub);
          return response;
        }

        public bool PostReply(ReplyCommentClubViewModel commentreply)
        {
            var curUserId = сontextAccessor?.HttpContext?.User.GetUserId();
            var curUserName = сontextAccessor?.HttpContext?.User?.Identity?.Name;

            var comment = mapper.Map<ReplyCommentClub>(commentreply);
            comment.InternalUserId = Guid.Parse(curUserId);
            comment.CreatedDate = DateTime.Now;
            comment.UserName = curUserName;

           var response = unitOfWorkRepository.ClubCommentRepository.AddReplyComment(comment);
           return response;
        }
    }
}
