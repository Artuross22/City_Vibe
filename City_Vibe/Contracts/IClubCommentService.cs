using City_Vibe.Services.Base;
using City_Vibe.ViewModels.ClubCommentController;

namespace City_Vibe.Contracts
{
    public interface IClubCommentService
    {
        bool PostComment(PostCommentClubViewModel comment);
        bool PostReply(ReplyCommentClubViewModel commentreply);

    }
}
