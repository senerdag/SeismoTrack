using SeismoTrack.Models;
using System.Text.Json;
using System.Text;

namespace SeismoTrack.Services
{
    public class EarthquakeService
    {
        private readonly HttpClient _httpClient;

        public EarthquakeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EarthquakeModel>> GetLast3DaysEarthquakesAsync()
        {
            var now = DateTime.UtcNow;
            var startDate = now.AddDays(-15).ToString("yyyy-MM-ddTHH:mm:ssZ");
            var endDate = now.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var payload = new
            {
                EventSearchFilterList = new[]
                {
                new { FilterType = 8, Value = startDate },
                new { FilterType = 9, Value = endDate }
            },
                Skip = 0,
                Take = 5000,
                SortDescriptor = new { field = "eventDate", dir = "desc" }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://deprem.afad.gov.tr/EventData/GetEventsByFilter", content);

            var responseJson = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseJson);
            var data = doc.RootElement.GetProperty("eventList");

            var list = new List<EarthquakeModel>();

            foreach (var item in data.EnumerateArray())
            {
                list.Add(new EarthquakeModel
                {
                    Depth = item.GetProperty("depth").GetDouble(),
                    Location = item.GetProperty("location").GetString(),
                    Magnitude = item.GetProperty("magnitude").GetDouble(),
                    EventDate = item.GetProperty("eventDate").GetDateTime(),
                    Latitude = item.GetProperty("latitude").GetDouble(),
                    Longitude = item.GetProperty("longitude").GetDouble()
                });
            }

            return list;
        }
    }
}
