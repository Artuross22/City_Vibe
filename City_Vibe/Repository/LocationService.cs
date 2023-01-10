using Microsoft.EntityFrameworkCore;
using City_Vibe.Data;
using City_Vibe.Helpers;
using City_Vibe.Interfaces;
using City_Vibe.Models;

namespace RunGroopWebApp.Services
{
    //public class LocationService : ILocationService
    //{
    //    private readonly ApplicationDbContext _context;

    //    public LocationService(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }


        //public async Task<City> GetCityByZipCode(int zipCod)
        //{
        //    return await _context.Addresses.FirstOrDefaultAsync(x => x.ZipCode == zipCod);
        //}

        //public async Task<List<City>> GetLocationSearch(string location)
        //{
        //    List<City> result;
        //    if (location.Length > 0 && char.IsDigit(location[0]))
        //    {
        //        return await _context..Where(x => x.ZipCode.ToString().StartsWith(location)).Take(4).Distinct().ToListAsync();
        //    }
        //    else if (location.Length > 0)
        //    {
        //        result = await _context.Cities.Where(x => x.CityName == location).Take(10).ToListAsync();
        //    }
        //    result = await _context.Cities.Where(x => x.StateCode == location).Take(10).ToListAsync();

        //    return result;
        //}
   // }
}
