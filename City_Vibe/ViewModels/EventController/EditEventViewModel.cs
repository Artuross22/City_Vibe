
using City_Vibe.Domain.Models;
using Microsoft.Build.Framework;

namespace City_Vibe.ViewModels.EventController
{
    public class EditEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AppUserId { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
        public string? URL { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public int CategoryId { get; set; }

        //public IEnumerable<SelectListItem> CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool? Succeeded { get; set; }
    }
}

