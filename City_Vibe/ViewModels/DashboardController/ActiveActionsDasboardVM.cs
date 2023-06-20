
using City_Vibe.Domain.Models;

namespace City_Vibe.ViewModels.DashboardController
{
    public class ActiveActionsDasboardVM
    {
        public List<Event> Events { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
    }
}
