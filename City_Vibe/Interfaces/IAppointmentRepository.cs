using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        ICollection<Appointment> GetAppointmentsByEventId(int eventId);
        ICollection<Appointment> GetAppointmentByIdUser(string Id);

        bool AddReplyAppointment(ReplyAppointment replyAppointment);
    }
}
