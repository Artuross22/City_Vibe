using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;

namespace City_Vibe.Repository
{
    public class SaveEventRepository : GenericRepository<SaveEvent> , ISaveEventRepository
    {

        private readonly ApplicationDbContext dbcontext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveEventRepository(ApplicationDbContext _context, IHttpContextAccessor httpContextAccess) : base(_context)
        {
            dbcontext = _context;
            httpContextAccessor = httpContextAccess;
        }

        public List<SaveEvent> FindSafeEventsinUserAndEvent(int Id)
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var result = dbcontext.SaveEvents.Where(c => c.AppUserId == curUserId).Where(x => x.EventId == Id).ToList();
            return result;

        }

        public async Task<IEnumerable<SaveEvent>> FindEventByIdAsync(int Id)
        {
            var result = dbcontext.SaveEvents.Where(c => c.EventId == Id);
            return result;
        }
    }
}
