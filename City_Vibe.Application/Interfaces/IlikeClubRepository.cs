using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IlikeClubRepository : IGenericRepository<LikeClub>
    {

        Task<List<LikeClub>> FindLikeByUserIdAndClubId(string curUserId, int clubId);
    }
}
