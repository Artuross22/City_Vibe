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

