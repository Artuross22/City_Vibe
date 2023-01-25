using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IlikeClubRepository
    {
        public int GetLikeClubsByClubId(int Id);
        Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId, int clubId);
        Task<LikeClub> FindLikeClubByUserId(string Id);
        bool Add(LikeClub eventLikeAdd);
        bool Update(LikeClub eventLikeUp);
        bool Delete(LikeClub eventLikeDelete);
        bool Save();
    }
}
