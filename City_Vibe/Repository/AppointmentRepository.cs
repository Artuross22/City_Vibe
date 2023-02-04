using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace City_Vibe.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentRepository(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public bool Add(Appointment appointmentAdd)
        {
            dbContext.Add(appointmentAdd);
            return Save();
        }

        public bool Delete(Appointment appointmentDe)
        {
            dbContext.Remove(appointmentDe);
            return Save();
        }

        public bool Update(Appointment appointmentUp)
        {
            dbContext.Update(appointmentUp);
            return Save();
        }

        public bool AddReplyAppointment(ReplyAppointment replyAppointment)
        {
            dbContext.Add(replyAppointment);
            return Save();
        }

        public bool Save()
        {
            var saved = dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Appointment> GetAppointmentsByEventId(int eventId)
        {
            var application = dbContext.Appointments.Where(e => e.EventId == eventId).Include(x => x.AppUser);
            return application.ToList();
        }

        public Appointment GetAppointmentByIdAsNoTracking(int appointmentId)
        {
            var result = dbContext.Appointments.AsNoTracking().FirstOrDefault(x => x.Id == appointmentId);
            return (result);
        }

        public ICollection<Appointment> GetAppointmentByIdUser(string curUserId)
        {
            var getappointment = dbContext.Appointments.Where(x => x.AppUserId == curUserId).Include(x => x.AppUser).Include(e => e.Event);
            return getappointment.ToList();
        }
    }
}
