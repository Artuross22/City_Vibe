using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.Infrastructure.Implement;
using Microsoft.AspNetCore.Http;

namespace City_Vibe.Infrastructure.Repository
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

        public List<SaveClub> FindSafeClubusingUserAndClub(int Id)
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var result = dbcontext.SaveClubs.Where(c => c.AppUserId == curUserId).Where(x => x.ClubId == Id).ToList();
            return result;
        }
    }
}
