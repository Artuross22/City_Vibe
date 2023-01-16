using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using SendGrid.Helpers.Mail;
using System.Collections;

namespace City_Vibe.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext context;

        public EventRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public bool Add(Event eventAdd)
        {
            context.Add(eventAdd);
            return Save();
        }

        public bool Delete(Event eventDe)
        {
            context.Remove(eventDe);
            return Save();
        }

        public bool Update(Event eventUp)
        {
            context.Update(eventUp);
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

    }
}

