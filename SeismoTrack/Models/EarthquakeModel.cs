using System.Text.Json.Serialization;

namespace SeismoTrack.Models
{
    public class EarthquakeModel
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }


        [JsonPropertyName("depth")]
        public double Depth { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("magnitude")]
        public double Magnitude { get; set; }
    }
}
