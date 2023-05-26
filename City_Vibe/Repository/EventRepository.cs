using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;

using Microsoft.EntityFrameworkCore;


namespace City_Vibe.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext context;

        public EventRepository(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<Event> GetByIdIncludeCategoryAndAddressAsync(int id)
        {
            return await context.Events.Include(i => i.Category).Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> GetByIdIncludeCommentsAsync(int id)
        {
            return await context.Events.Include(i => i.Category).Include(x => x.Address).Include(c => c.Comments).ThenInclude(x => x.ReplyComment).OrderByDescending(x => x.Data).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await context.Events.Include(i => i.Category).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Event>> ActiveEventBytime()
        {
            DateTime dateTime = DateTime.Now;

            return await context.Events.Where(x => x.Data >= dateTime).OrderByDescending(x => x.Data).ToListAsync();

        }

        public IQueryable<Event> ActiveEventBytimeIQueryable()
        {
            DateTime dateTime = DateTime.Now;
            return context.Events.Where(x => x.Data >= dateTime).Include(x => x.Category).OrderByDescending(x => x.Data);
        }
        public  IQueryable<Event> ActiveEventAllIQueryable()
        {
            return  context.Events.Include(x => x.Category).OrderByDescending(x => x.Data);
        }

        public int CheckingTheExistenceOfAnAppointment(int currentEventId, string currentUserId)
        {
            var сheckAppointment =  context.Appointments.Where(x => x.AppUserId == currentUserId).Where(e => e.EventId == currentEventId).ToList().Count();
            return сheckAppointment;
        }

        public Appointment ReplyAppointment(int currentEventId, string currentUserId)
        {
            return context.Appointments.Include(x => x.ReplyAppointments).FirstOrDefault(x => x.AppUserId == currentUserId && x.EventId == currentEventId);         
        }

        
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}

