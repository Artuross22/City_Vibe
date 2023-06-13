using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        private readonly ApplicationDbContext contextDb;

        public CommentRepository(ApplicationDbContext context) : base(context) => contextDb = context;
   
        public  ICollection<Comment> GetAllCommentByEventId(int id)
        {
           return contextDb.Comments.Where(x => x.EventId == id).Include(x => x.ReplyComment).ThenInclude(x => x.AppUser).OrderByDescending(x => x.DateTime).ToList();
        }

        public bool AddReplyComment(ReplyComment replyComment)
        {
            contextDb.Add(replyComment);
            return Save();
        }

        public bool Save()
        {
            var saved = contextDb.SaveChanges();
            return saved > 0 ? true : false;
        }
   
    }
}

