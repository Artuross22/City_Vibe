
using City_Vibe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Domain.Interfaces
{
    public interface ICityVibeDbContext
    {
        public DbSet<CommentClub> CommentClubs { get; set; }

        public DbSet<ReplyCommentClub> ReplyCommentClubs { get; set; }

        public DbSet<ReplyAppointment> ReplyAppointments { get; set; }

        public DbSet<PostInfoInClub> PostInfoInClub { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ReplyAppointment> ReplyAppointment { get; set; }

        public DbSet<LikeClub> LikeClubs { get; set; }

        public DbSet<SaveEvent> SaveEvents { get; set; }

        public DbSet<Club> Club { get; set; }

        public DbSet<SaveClub> SaveClubs { get; set; }

        public DbSet<AppUser> AppUser { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ReplyComment> Replies { get; set; }
    }
}
