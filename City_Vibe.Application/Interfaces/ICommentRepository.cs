using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {

        bool AddReplyComment(ReplyComment replyComment);
        bool Save();
    }
}
