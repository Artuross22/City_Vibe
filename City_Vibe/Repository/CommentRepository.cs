using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext contextDb;

        public CommentRepository(ApplicationDbContext context)
        {
            contextDb = context;
        }

        public bool Add(Comment comment)
        {
            contextDb.Add(comment);
            return Save();
        }

        public bool Delete(Comment comment)
        {
            contextDb.Remove(comment);
            return Save();
        }

        public bool Update(Comment comment)
        {
            contextDb.Update(comment);
            return Save();
        }

        public bool AddReplyComment(ReplyComment replyComment)
        {
            contextDb.Add(replyComment);
            return Save();
        }

     


        public  ICollection<Comment> GetAllCommentByEventId(int id)
        {
            var commentsModel = contextDb.Comments.Where(x => x.EventId == id).Include(x => x.ReplyComment).ThenInclude(x => x.AppUser).OrderByDescending(x => x.DateTime).ToList();
            return  commentsModel;
        }

        public IEnumerable<Comment> GetAll()
        {
            return  contextDb.Comments.ToList();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await contextDb.Comments.Include(i => i.ReplyComment).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Comment> GetByIdAsyncNoTracking(int id)
        {
            return await contextDb.Comments.Include(i => i.ReplyComment).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

   
        public bool Save()
        {
            var saved = contextDb.SaveChanges();
            return saved > 0 ? true : false;
        }

   
    }
}

