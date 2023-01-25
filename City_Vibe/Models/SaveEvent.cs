
﻿using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class SaveEvent
    {
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        [NotMapped]
        public ICollection<Event> Events { get; set; }
    }
}
