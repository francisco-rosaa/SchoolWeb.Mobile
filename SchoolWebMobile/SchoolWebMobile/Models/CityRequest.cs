using Newtonsoft.Json;

namespace SchoolWebMobile.Models
{
    public class CityRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
