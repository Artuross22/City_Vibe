using City_Vibe.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.ViewModels.EventController
{
    public class EventFilterViewModel
    {
        public IEnumerable<Event> Events { get; set; } = new List<Event>();

        public SelectList Category { get; set; } = new SelectList(new List<Category>(), "Id", "Name");
        public string? Name { get; set; }
    }
}
