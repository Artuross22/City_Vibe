using City_Vibe.Models;

namespace City_Vibe.ViewModels.Categories
{
    public class CategoriesAddViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Event>? Events { get; set; }
    }
}
