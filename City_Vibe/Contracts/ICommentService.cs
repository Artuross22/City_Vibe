using City_Vibe.Services.Base;
using City_Vibe.ViewModels.CommentController;

namespace City_Vibe.Contracts
{
    public interface ICommentService
    {
        bool PostComment(PostCommentViewModel Comment);
        bool PostReply(ReplyViewModel commentreply);

    }
}
