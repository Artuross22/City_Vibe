using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Infrastructure.Repository
{
    public class likeClubRepository : GenericRepository<LikeClub>, IlikeClubRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public likeClubRepository(ApplicationDbContext applicationDbcontext) : base(applicationDbcontext) => applicationDb = applicationDbcontext;

        public async Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId, int clubId)
        {
           return await applicationDb.LikeClubs.Where(u => u.AppUserId == curUserId).Where(c => c.ClubId == clubId).ToListAsync();        
        }

    }
}
