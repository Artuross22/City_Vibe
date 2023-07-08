using City_Vibe.Services.Base;
using City_Vibe.ViewModels.CommentController;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface ICommentService
    {
        Response PostComment(PostCommentViewModel Comment);
        Response PostReply(ReplyViewModel commentreply);

    }
}
