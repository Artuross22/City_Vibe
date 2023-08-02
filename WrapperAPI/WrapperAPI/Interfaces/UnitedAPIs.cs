
using City_Vibe.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using WrapperAPI.APIs.CryptoAPI.Interfaces;
using WrapperAPI.APIs.CryptoAPI.Realization;
using WrapperAPI.APIs.WeatherAPI.Interfaces;
using WrapperAPI.APIs.WeatherAPI.Realization;

namespace WrapperAPI.BaseAPI.Interfaces
{
    public class UnitedAPIs : IUnitedAPIs
    {
        public UnitedAPIs(IOptions<SettingsKeyAPI> options)
        {

            WeatherAPIImplementation = new WeatherApiAdapter(options);
            CryptoAPIImplementation = new CryptoApiAdapter(options);
        }

        public IWeatherApiAdapter WeatherAPIImplementation { get; private set; }

        public ICryptoApiAdapter CryptoAPIImplementation { get; private set; }

    }
}
