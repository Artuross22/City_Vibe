using City_Vibe.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using WrapperAPI.APIs.WeatherAPI.Interfaces;
using WrapperAPI.APIs.WeatherAPI.Models;
using WrapperAPI.BaseAPI;


namespace WrapperAPI.APIs.WeatherAPI.Realization
{
    public  class  WeatherApiAdapter : IWeatherApiAdapter
    {
        private readonly APIWrapper apiWrapper;
        const string baseHostUrl = "https://the-weather-api.p.rapidapi.com";

        public WeatherApiAdapter(IOptions<SettingsKeyAPI> options)
        {
             apiWrapper = new APIWrapper(baseHostUrl, options.Value.CriptoHeaders);
        }

        public  async Task<WeatherApiResponse> WeatherHandlerAPI(string city)
        {
            if(String.IsNullOrEmpty(city)) city = "Chernivtsi,%20Chernivtsi%20Oblast,%20Ukraine";
            string uri = $"api/weather/{city}'";

            var response = await apiWrapper.Get<WeatherApiResponse>(uri);
            return response;
        }
    }
}
