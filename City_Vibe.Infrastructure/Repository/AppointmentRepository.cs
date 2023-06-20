using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Infrastructure.Repository
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
