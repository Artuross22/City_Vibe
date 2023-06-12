using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class DashboardRepository : IDashboardRepository
    {

        private readonly ApplicationDbContext contextDb;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccess)
        {
            contextDb = context;
            httpContextAccessor = httpContextAccess;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = contextDb.Club.Where(r => r.AppUser.Id == curUser);
            return userClubs.ToList();
        }

        public async Task<List<Event>> GetAllUserEvent()
        {
            var curUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userEvent = contextDb.Events.Where(r => r.AppUser.Id == curUser);
            return userEvent.ToList();
        }
    }
}
