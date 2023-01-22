using City_Vibe.Models;
using System.Linq;

namespace City_Vibe.Interfaces
{
    public interface ISaveClubRepository
    {
         ICollection<SaveClub> FindUserById(string Id);

        Task<IEnumerable<SaveClub>> FindClubsByIdAsync(int Id);
        Task<SaveClub> FindClubById(int Id);
        List<SaveClub> findSafeClubusingUserAndClub(int Id);
        bool Add(SaveClub saveclubAdd);
        bool Update(SaveClub saveclubtUp);
        bool Delete(SaveClub saveclubDelete);
        bool Save();
    }
}
