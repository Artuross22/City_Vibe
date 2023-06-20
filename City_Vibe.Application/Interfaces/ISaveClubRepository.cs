using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface ISaveClubRepository : IGenericRepository<SaveClub>
    {
        List<SaveClub> FindSafeClubusingUserAndClub(int Id);

    }
}
