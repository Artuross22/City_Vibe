using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;


namespace City_Vibe.Repository
{
    public class SaveClubRepository : ISaveClubRepository
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveClubRepository(ApplicationDbContext _context, IHttpContextAccessor httpContextAccess)
        {
            dbcontext = _context;
            httpContextAccessor = httpContextAccess;
        }

        public async Task<SaveClub> FindClubById(int Id)
        {
            var result  = await  dbcontext.SaveClubs.FirstOrDefaultAsync(c => c.ClubId == Id);
            return result;
        }

        public async Task<IEnumerable<SaveClub>> FindClubsByIdAsync(int Id)
        {
            var result = dbcontext.SaveClubs.Where(c => c.ClubId == Id);
            return result;
        }

        public List<SaveClub> FindSafeClubusingUserAndClub(int Id)
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var result = dbcontext.SaveClubs.Where(c => c.AppUserId == curUserId).Where(x => x.ClubId == Id).ToList();
            return result;

        }


        public ICollection<SaveClub> FindUserById(string curUserId)
        {
            var result = dbcontext.SaveClubs.Where(x => x.AppUserId == curUserId).Include(x => x.Club).ToList();
            return result;
        }


        public bool Add(SaveClub saveClubeAdd)
        {
            dbcontext.Add(saveClubeAdd);
            return Save();
        }

        public bool Delete(SaveClub eventDe)
        {
            dbcontext.Remove(eventDe);
            return Save();
        }

        public bool Update(SaveClub eventUp)
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
