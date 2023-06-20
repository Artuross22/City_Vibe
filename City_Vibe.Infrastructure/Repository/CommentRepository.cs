using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;


namespace City_Vibe.Infrastructure.Repository
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

