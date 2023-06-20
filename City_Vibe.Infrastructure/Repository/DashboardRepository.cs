using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.ExtensionMethod;
using Microsoft.AspNetCore.Http;

namespace City_Vibe.Infrastructure.Repository
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
