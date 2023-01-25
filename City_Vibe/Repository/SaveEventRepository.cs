using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class SaveEventRepository : ISaveEventRepository
    {

        private readonly ApplicationDbContext dbcontext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveEventRepository(ApplicationDbContext _context, IHttpContextAccessor httpContextAccess)
        {
            dbcontext = _context;
            httpContextAccessor = httpContextAccess;
        }

        public async Task<SaveEvent> FindEventById(int Id)
        {
            var result = await dbcontext.SaveEvents.FirstOrDefaultAsync(c => c.EventId == Id);
            return result;
        }

        public async Task<IEnumerable<SaveEvent>> FindEventByIdAsync(int Id)
        {
            var result = dbcontext.SaveEvents.Where(c => c.EventId == Id);
            return result;
        }

        public List<SaveEvent> FindSafeEventsinUserAndEvent(int Id)
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var result = dbcontext.SaveEvents.Where(c => c.AppUserId == curUserId).Where(x => x.EventId == Id).ToList();
            return result;

        }


        public ICollection<SaveEvent> FindEventById(string curUserId)
        {
            var selectEvent = dbcontext.SaveEvents.Where(x => x.AppUserId == curUserId).Include(c => c.Event).ToList();
            return selectEvent;
        }



        public bool Add(SaveEvent saveEventAdd)
        {
            dbcontext.Add(saveEventAdd);
            return Save();
        }

        public bool Delete(SaveEvent eventDe)
        {
            dbcontext.Remove(eventDe);
            return Save();
        }

        public bool Update(SaveEvent eventUp)
        {
            dbcontext.Update(eventUp);
            return Save();
        }

        public bool Save()
        {
            var saved = dbcontext.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
