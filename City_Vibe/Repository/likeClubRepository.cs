 using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class likeClubRepository : GenericRepository<LikeClub>, IlikeClubRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public likeClubRepository(ApplicationDbContext applicationDbcontext) : base(applicationDbcontext) => applicationDb = applicationDbcontext;

        public async Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId, int clubId)
        {
           return await applicationDb.LikeClubs.Where(u => u.AppUserId == curUserId).Where(c => c.ClubId == clubId).ToListAsync();        
        }

        public int GetLikeClubsByClubId(int Id)
        {
            return applicationDb.LikeClubs.Where(x => x.ClubId == Id).ToList().Count();      
        }
    }
}
