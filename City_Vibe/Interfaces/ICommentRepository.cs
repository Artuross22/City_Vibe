using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {

        bool AddReplyComment(ReplyComment replyComment);
        bool Save();
    }
}
