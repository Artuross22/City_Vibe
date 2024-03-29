﻿using City_Vibe.Domain.Interfaces;
using City_Vibe.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace City_Vibe.Infrastructure.Data
{
    public class ApplicationDbContext :  IdentityDbContext<AppUser> , ICityVibeDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

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
        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<ReplyComment> Replies { get; set; } = null!;   

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .Ignore(p => p.RoleId);

            builder.Entity<AppUser>()
                .Ignore(p => p.Role);

            builder.Entity<AppUser>()
                .Ignore(p => p.RoleList);


            //builder.Entity<Club>().HasKey(x => new )
         }
    }
}
