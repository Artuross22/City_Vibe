using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IClubRepository : IGenericRepository<Club>
    {

        Task<Club> GetByIdIncludeAddressAsync(int id);

        Task<Club> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);
        Task<IEnumerable<Event>> GetClubsByEventId(int id);
        Task<int> GetCountAsync();
        Task <IEnumerable<PostInfoInClub>> GetPostInfoInClubByClubId(int id);
        bool AddPostInfoInClub(PostInfoInClub postInfoInClub);
        Task<PostInfoInClub> FindByIdPostInfo(int id);

        Task<IEnumerable<Club>> GetSliceAsync(int offset, int size);
        Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(Category category, int offset, int size);
        Task<int> GetCountByCategoryAsync(Category category);

        bool Save();
    }
}
