using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAll();
        public  Task<Comment> GetByIdAsync(int id);
        Task<Comment> GetByIdAsyncNoTracking(int id);
        ICollection<Comment> GetAllCommentByEventId(int id);

        bool AddReplyComment(ReplyComment replyComment);
        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}
