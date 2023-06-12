using City_Vibe.Models;
using System.Linq;

namespace City_Vibe.Interfaces
{
    public interface ISaveClubRepository : IGenericRepository<SaveClub>
    {

        Task<IEnumerable<SaveClub>> FindClubsByIdAsync(int Id);
        List<SaveClub> FindSafeClubusingUserAndClub(int Id);

    }
}
