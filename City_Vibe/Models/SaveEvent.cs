using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class SaveEvent
    {
        public int Id { get; set; }

        public AppUser User { get; set; }
        public string UserId { get; set; }

        public Event Event { get; set; }
        public int EventId { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public ICollection<Club>? Clubs { get; set; }
    }
}
