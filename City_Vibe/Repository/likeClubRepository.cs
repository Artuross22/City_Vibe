using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class likeClubRepository : IlikeClubRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public likeClubRepository(ApplicationDbContext applicationDbcontext)
        {
           applicationDb = applicationDbcontext;
        }

        public async Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId,int clubId)
        {
            var checkLikes = await applicationDb.LikeClubs.Where(u => u.AppUserId == curUserId).Where(c => c.ClubId == clubId).ToListAsync();
            return checkLikes;
        }

        public  int GetLikeClubsByClubId(int Id)
        {        
            var result =  applicationDb.LikeClubs.Where(x => x.ClubId == Id).ToList().Count();
            return result;
        }

        public async Task<LikeClub> FindLikeClubByUserId(string curUserId)
        {
            var result = await applicationDb.LikeClubs.FirstOrDefaultAsync(x => x.AppUserId == curUserId);
            return result;
        }

        public bool Add(LikeClub eventLikeAdd)
        {
            applicationDb.Add(eventLikeAdd);
            return Save();
        }

        public bool Delete(LikeClub eventLikeDe)
        {
            applicationDb.Remove(eventLikeDe);
            return Save();
        }

        public bool Update(LikeClub eventLikeUp)
        {
            applicationDb.Update(eventLikeUp);
            return Save();
        }

        public bool Save()
        {
            var saved = applicationDb.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
