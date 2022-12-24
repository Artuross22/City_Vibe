using Microsoft.Extensions.Logging;

namespace City_Vibe.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Event>? Events { get; set; }
    }
}
