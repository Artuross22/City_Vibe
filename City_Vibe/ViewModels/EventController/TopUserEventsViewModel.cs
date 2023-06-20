

using City_Vibe.Domain.Models;

namespace City_Vibe.ViewModels.EventController
{
    public class TopUserEventsViewModel
    {
        public int EventId { get; set; }   
        public List<SaveEvent> SaveEvents { get; set; }
    }
}
