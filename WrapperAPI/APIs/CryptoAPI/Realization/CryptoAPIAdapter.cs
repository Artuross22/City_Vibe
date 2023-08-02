using City_Vibe.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using WrapperAPI.APIs.CryptoAPI.Interfaces;
using WrapperAPI.APIs.CryptoAPI.Models;
using WrapperAPI.BaseAPI;


namespace WrapperAPI.APIs.CryptoAPI.Realization
{
    public class CryptoApiAdapter : ICryptoApiAdapter
    {
        public SettingsKeyAPI Options { get; set; }
        private readonly APIWrapper apiWrapper;
        const string baseHostUrl = "https://coinranking1.p.rapidapi.com";

        public CryptoApiAdapter(IOptions<SettingsKeyAPI> options )
        {
            Options = options.Value;
            apiWrapper = new APIWrapper(baseHostUrl, options.Value.CriptoHeaders);
        }


        public async Task<CryptoCoinAPI> CryptoHandlerAPIsd()
        {
            string uri = "coins?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h&tiers%5B0%5D=1&orderBy=marketCap&orderDirection=desc&limit=2&offset=0";
            var request = await apiWrapper.Get<CryptoCoinAPI>(uri);
            return request;          
        }
    }
} 
