using City_Vibe.Domain.Models;


namespace City_Vibe.Application.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        ICollection<Appointment> GetAppointmentsByEventId(int eventId);

        bool AddReplyAppointment(ReplyAppointment replyAppointment);
    }
}

