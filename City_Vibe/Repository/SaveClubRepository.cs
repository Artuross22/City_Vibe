using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;

namespace City_Vibe.Repository
{
    public class SaveClubRepository : GenericRepository<SaveClub> ,  ISaveClubRepository
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveClubRepository(ApplicationDbContext _context, IHttpContextAccessor httpContextAccess) : base(_context)
        {
            dbcontext = _context;
            httpContextAccessor = httpContextAccess;
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
    }
}
