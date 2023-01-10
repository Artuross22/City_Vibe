﻿using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ILocationService
    {

        Task<List<City>> GetLocationSearch(string location);
        Task<City> GetCityByZipCode(int zipCode);
    }
}
