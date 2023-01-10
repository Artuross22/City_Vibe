using City_Vibe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace City_Vibe.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Club> Club { get; set; }


       // public DbSet<Address> Addresses { get; set; } = null!;

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
         }
    }
}
