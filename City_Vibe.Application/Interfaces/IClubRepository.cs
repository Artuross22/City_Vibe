using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IClubRepository : IGenericRepository<Club>
    {

        Task<Club> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

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
