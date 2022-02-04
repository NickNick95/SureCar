using Newtonsoft.Json;

namespace SureCar.DataManager.Models
{
    /// <summary>
    /// The Location model
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The Latitude
        /// </summary>
        [JsonProperty("lat")]
        public string Latitude { get; set; }
        /// <summary>
        /// The Longitude
        /// </summary>
        [JsonProperty("long")]
        public string Longitude { get; set; }
    }
}
