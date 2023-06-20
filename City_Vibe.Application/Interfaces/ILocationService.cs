
using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface ILocationService
    {

        Task<List<City>> GetLocationSearch(string location);
        Task<City> GetCityByZipCode(int zipCode);
    }
}
