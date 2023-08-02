
namespace City_Vibe.Infrastructure.Helpers
{
    public class SettingsKeyAPI
    {
        public RapidApiHeaders CriptoHeaders { get; set; }

        public RapidApiHeaders WeatherHeaders { get; set; }
    }

    public class RapidApiHeaders
    {
        public string? ApiKey { get; set; }

        public string? ApiHost { get; set; }
    }
}
