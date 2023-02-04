using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<Appointment> GetAppointmentsByEventId(int eventId);
        Appointment GetAppointmentByIdAsNoTracking(int eventId);
        ICollection<Appointment> GetAppointmentByIdUser(string Id);

        bool AddReplyAppointment(ReplyAppointment replyAppointment);
        bool Add(Appointment appointment);
        bool Update(Appointment appointment);
        bool Delete(Appointment appointment);
        bool Save();
    }
}
