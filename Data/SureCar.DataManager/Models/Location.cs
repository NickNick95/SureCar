using Newtonsoft.Json;

namespace SureCar.DataManagers.Models
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
        public double Latitude { get; set; }
        /// <summary>
        /// The Longitude
        /// </summary>
        [JsonProperty("long")]
        public double Longitude { get; set; }
    }
}
