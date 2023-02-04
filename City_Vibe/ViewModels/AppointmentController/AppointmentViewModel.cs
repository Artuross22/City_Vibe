using City_Vibe.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.AppointmentController
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Phone]
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? AppUserId { get; set; }
        public int? EventId { get; set; }
    }
}
