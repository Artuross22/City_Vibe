using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentRepository(ApplicationDbContext _context) : base(_context) => dbContext = _context;
      
        public ICollection<Appointment> GetAppointmentsByEventId(int eventId)
        {
            var application = dbContext.Appointments.Where(e => e.EventId == eventId).Include(x => x.AppUser);
            return application.ToList();
        }

        public ICollection<Appointment> GetAppointmentByIdUser(string curUserId)
        {
            var getappointment = dbContext.Appointments.Where(x => x.AppUserId == curUserId).Include(x => x.AppUser).Include(e => e.Event);
            return getappointment.ToList();
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
    }
}
