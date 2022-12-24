using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext context;

        public EventRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public bool Add(Event club)
        {
            context.Add(club);
            return Save();
        }

        public bool Delete(Event club)
        {
            context.Remove(club);
            return Save();
        }

        public bool Update(Event club)
        {
            context.Update(club);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await context.Events.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
         return await context.Events.Include(i => i.Category).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }    
       
    }
}
