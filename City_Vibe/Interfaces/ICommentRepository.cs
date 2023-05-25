using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        public  Task<Comment> GetByIdIncludeReplyCommentAsync(int id);
        Task<Comment> GetByIdAsyncNoTracking(int id);
        ICollection<Comment> GetAllCommentByEventId(int id);
        bool AddReplyComment(ReplyComment replyComment);
        bool Save();
    }
}
