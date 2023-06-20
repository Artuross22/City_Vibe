
using System.ComponentModel.DataAnnotations.Schema;

 namespace City_Vibe.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Phone { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [ForeignKey("Event")]
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public bool Statement { get; set; }
        public ICollection<ReplyAppointment>? ReplyAppointments { get; set; }
    }
}