using WrapperAPI.APIs.CryptoAPI.Interfaces;
using WrapperAPI.APIs.WeatherAPI.Interfaces;

namespace WrapperAPI.BaseAPI.Interfaces
{
    public interface IUnitedAPIs
    {
        IWeatherApiAdapter WeatherAPIImplementation { get; }
        ICryptoApiAdapter CryptoAPIImplementation { get; }
    }
}
