﻿using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Domain.Models
{
    public class ReplyComment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }


        [ForeignKey("User")]
        public Guid? InternalUserId { get; set; }
        public  AppUser? AppUser { get; set; }


        public int CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
