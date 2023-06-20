
using City_Vibe.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.ViewModels.ClubController
{
    public class IndexClubViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalClubs { get; set; }
        public SelectList Category { get; set; } = new SelectList(new List<Category>(), "Id", "Name");
        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
