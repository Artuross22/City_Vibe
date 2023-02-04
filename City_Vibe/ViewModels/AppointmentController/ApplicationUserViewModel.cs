using City_Vibe.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.AppointmentController
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Email { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Phone { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? EventId { get; set; }

        public bool Statement { get; set; }
        public ICollection<ReplyAppointment>? ReplyAppointments { get; set; }

    }
}
