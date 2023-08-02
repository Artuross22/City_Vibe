
using WrapperAPI.APIs.WeatherAPI.Models;

namespace WrapperAPI.APIs.WeatherAPI.Interfaces
{
    public interface IWeatherApiAdapter
    {
        Task<WeatherApiResponse> WeatherHandlerAPI(string city);
    }
}
