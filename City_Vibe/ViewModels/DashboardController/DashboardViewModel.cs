

using City_Vibe.Domain.Models;

namespace City_Vibe.ViewModels.DashboardController
{
    public class DashboardViewModel
    {
       
        public List<Event> Events { get; set; }
        public List<Club> Clubs { get; set; }
    }
}
