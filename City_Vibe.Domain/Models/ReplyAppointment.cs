using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Domain.Models
{
    public class ReplyAppointment
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Reason { get; set; }

        [ForeignKey("Appointment")]
        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }


        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        [ForeignKey("Event")]
        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}
