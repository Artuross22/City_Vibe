using City_Vibe.Models;

namespace City_Vibe.ViewModels.EventController
{
    public class TopUserEventsViewModel
    {
        public int EventId { get; set; }   
        public List<SaveEvent> SaveEvents { get; set; }
    }
}
