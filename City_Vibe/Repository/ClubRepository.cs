using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace City_Vibe.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Club.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Club.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Club.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Club.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task< IEnumerable<Event>> GetClubsByEventId(int id)
        {
            var result = _context.Events.Where(c => c.ClubId == id).Include(x => x.Address).Include( x=> x.Category).ToList();
            return result;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
