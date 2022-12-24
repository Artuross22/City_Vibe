using System.Xml.Linq;

namespace City_Vibe.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Desciption { get; set; }

        public DateTime? Data { get; set; }

        public string? City { get; set; }

        public int CategoryId { get; set; }


        public Category? Category { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public Event()
        {
            Comments = new List<Comment>();
        }
    }
}
