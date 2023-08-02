using City_Vibe.Infrastructure.Helpers;
using System.Text.Json;

namespace WrapperAPI.BaseAPI
{
    public class APIWrapper
    {
        public RapidApiHeaders Headers { get; set; }
        public string baseUrl { get; set; }
        public APIWrapper(string _baseUrl, RapidApiHeaders headers )
        {
            Headers = headers;
            baseUrl = _baseUrl;
        }

        public async Task<T> Get<T>(string uri) where T : class
        {
            var requestUrl = $"{baseUrl}/{uri}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri(requestUrl),
                Headers =
                {
                  { "X-RapidAPI-Key", Headers.ApiKey  },
                  { "X-RapidAPI-Host", Headers.ApiHost },
                },
            };

            var client = new HttpClient();

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync(); //.Result;

                T? result = JsonSerializer.Deserialize<T>(body);
                return result;
            }
        }
    }
}
