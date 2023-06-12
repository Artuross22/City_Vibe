using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IlikeClubRepository : IGenericRepository<LikeClub>
    {
        public int GetLikeClubsByClubId(int Id);

        Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId, int clubId);
    }
}
